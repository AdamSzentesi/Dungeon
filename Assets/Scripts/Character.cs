using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None,
    North,
    South,
    East,
    West,
}

public class Character : Tileable
{
    public float Speed = 0.5f;
    public int Health = 100;
    public bool IsAlive { get; private set; } = true;

    // TODO: inventory
    public bool HasItemKey { get; private set; } = false;

    private Coroutine _MoveCoroutine;
    private bool _IsMoving = false;
    private Direction _CurrentDirection = Direction.North;
    public Vector2Int _CurrentDirectionVector = new Vector2Int();

    private bool _IsStopped = false;

    private static Dictionary<Direction, Vector2Int>  _DirectionVectors = new Dictionary<Direction, Vector2Int>
    {
        { Direction.None, new Vector2Int(0, 0) },
        { Direction.North, new Vector2Int(0, 1) },
        { Direction.South, new Vector2Int(0, -1) },
        { Direction.East, new Vector2Int(1, 0) },
        { Direction.West, new Vector2Int(-1, 0) },
    };

    private static Dictionary<Direction, Direction> _TurnRightDirections = new Dictionary<Direction, Direction>
    {
        { Direction.None, Direction.None },
        { Direction.North, Direction.East },
        { Direction.South, Direction.West },
        { Direction.East, Direction.South },
        { Direction.West, Direction.North },
    };

    private void Start()
    {
        _CurrentDirection = Direction.North;
    }

    private void Update()
    {
        if (!IsAlive) return;
        if (_IsMoving) return;

        if (TryToMove()) return;

        // TODO: this is ugly and dumb
        _CurrentDirection = _TurnRightDirections[_CurrentDirection];
        if (TryToMove()) return;
        _CurrentDirection = _TurnRightDirections[_CurrentDirection];
        if (TryToMove()) return;
        _CurrentDirection = _TurnRightDirections[_CurrentDirection];
        if (TryToMove()) return;
    }

    private bool TryToMove()
    {
        Vector2Int desiredDirectionVector = _DirectionVectors[_CurrentDirection];
        Vector2Int desiredTilePosition = TilePosition + desiredDirectionVector;

        if (!Level.Instance.IsTilePositionValid(desiredTilePosition)) return false;

        Level.Instance.Dispatch(TileEvent.OnDesire, desiredTilePosition, this);

        if (_IsStopped)
        {
            _IsStopped = false;
            return false;
        }

        // TODO: inventory
        Item item = Level.Instance.GetBlockingItem(desiredTilePosition);
        if (item)
        {
            if (!item.Interact(this)) return false;
        }

        _MoveCoroutine = StartCoroutine(MoveCouroutine(desiredTilePosition));
        return true;
    }

    private IEnumerator MoveCouroutine(Vector2Int desiredTilePosition)
    {
        _IsMoving = true;

        Vector2 moveDirection = desiredTilePosition - TilePosition;
        float targetDistanceSquared = moveDirection.sqrMagnitude;
        moveDirection.Normalize();
        moveDirection *= Speed;
        Vector3 oldPosition = transform.localPosition;

        while ((oldPosition - transform.localPosition).sqrMagnitude < targetDistanceSquared)
        {
            transform.localPosition += new Vector3(moveDirection.x, moveDirection.y, 0) * Time.deltaTime; 
            yield return null;
        }
        
        SetTilePosition(desiredTilePosition);
        _IsMoving = false;
    }

    protected override void OnTilePositionChange()
    {
        Level.Instance.Dispatch(TileEvent.OnEnter, TilePosition, this);

        // TODO: inventory
        Item pickup = Level.Instance.PickupItem(TilePosition);
        if (pickup && pickup.ItemType == ItemType.Key)
        {
            HasItemKey = true;
        }
    }

    private void LateUpdate()
    {
        if (Health <= 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 90);
            IsAlive = false;
            OnDeath();
        }
    }

    public void Die()
    {
        Health = 0;
    }

    public void Stop()
    {
        _IsStopped = true;
    }

    private void OnDestroy()
    {
        if(_MoveCoroutine != null) StopCoroutine(_MoveCoroutine);
    }

    // Events

    protected virtual void OnDeath() { }

}
