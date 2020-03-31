using UnityEngine;

public class Tileable : MonoBehaviour
{
    public Vector2Int TilePosition = new Vector2Int();
    public SpriteRenderer TileSpriteRenderer;

    protected LevelBase Owner;

    public void SetTilePosition(Vector2Int desiredTilePosition)
    {
        if (!Level.Instance.IsTilePositionValid(desiredTilePosition)) return;

        TilePosition = desiredTilePosition;
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y);

        OnTilePositionChange();
    }

    protected virtual void OnTilePositionChange() { }


}
