using System;
using UnityEngine;

public enum TileType
{
    Floor,
    Wall,
    Door,
}

public class Tile : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private Tileset _Tileset;

    public TileType TileType;
    public Vector2Int TilePosition = new Vector2Int(); // TODO: private?
    
    public bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Init(Vector2Int tilePosition, TileType tileType, Tileset tileset, Transform parentTransform)
    {
        if (IsInitialized) return;

        _Tileset = tileset;
        TilePosition = tilePosition;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y, 2.0f);
        TileType = tileType;
        UpdateSprite();

        IsInitialized = true;
    }

    // DEBUG
    private void OnValidate()
    {
        if (Application.isPlaying) { UpdateSprite(); }
    }

    private void UpdateSprite()
    {
        _SpriteRenderer.sprite = _Tileset.GetSprite(TileType);
    }

}
