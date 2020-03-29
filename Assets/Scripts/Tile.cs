using UnityEngine;

public enum TileType
{
    Floor,
    Wall,
    Lava,
}

public class Tile : Tileable
{
    public TileType TileType;
    public bool IsInitialized { get; private set; } = false;

    public void Init(Vector2Int tilePosition, TileType tileType, Transform parentTransform, LevelBase owner)
    {
        if (IsInitialized) return;

        TilePosition = tilePosition;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y, 2.0f);
        TileType = tileType;
        Owner = owner;
        UpdateSprite();

        IsInitialized = true;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void UpdateSprite()
    {
        if (!Owner) return;
        SpriteRenderer.sprite = Owner.GetSprite(TileType);
    }

    private void OnValidate()
    {
        UpdateSprite();
    }

}
