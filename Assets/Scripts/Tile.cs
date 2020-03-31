using System;
using UnityEngine;

public enum TileType
{
    Floor,
    Wall,
    Lava,
}

public enum TileEvent
{
    OnEnter,
    OnDesire,
}

public class Tile : Tileable
{
    public TileType TileType;
    public bool IsInitialized { get; private set; } = false;

    private TileBehavior[] _TileBehaviors = new TileBehavior[Enum.GetNames(typeof(TileEvent)).Length];

    public void Init(Vector2Int tilePosition, TileType tileType, Transform parentTransform, LevelBase owner)
    {
        if (IsInitialized) return;

        TilePosition = tilePosition;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y, 2.0f);
        TileType = tileType;
        Owner = owner;

        UpdateSprite();
        LoadTileBehaviors();

        IsInitialized = true;
    }

    private void LoadTileBehaviors()
    {
        foreach (TileEvent tileEvent in Enum.GetValues(typeof(TileEvent)))
        {
            _TileBehaviors[(int)tileEvent] = TileBehaviorDatabase.CreateTileBehavior(TileType, tileEvent);
        }
    }

    public void OnTileEvent(TileEvent tileEvent, Character character)
    {
        TileBehavior tileBehavior = _TileBehaviors[(int)tileEvent];
        if (tileBehavior != null) tileBehavior.Execute(character, this);
    }

    // TODO: this might be a bad idea: I better make some prefabs and swap them
    public void SetTileType(TileType tileType)
    {
        TileType = tileType;
        UpdateSprite();
        LoadTileBehaviors();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void UpdateSprite()
    {
        if (!Owner) return;
        TileSpriteRenderer.sprite = Owner.GetSprite(TileType);
    }

    private void OnValidate()
    {
        UpdateSprite();
    }

}
