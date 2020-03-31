using UnityEngine;

public enum ItemType
{
    None,
    Key,
    Lock,
    Exit,
}

public class Item : Tileable
{
    public ItemType ItemType;
    public bool IsInitialized { get; private set; } = false;

    public void Init(Vector2Int tilePosition, ItemType itemType, Transform parentTransform, Sprite sprite)
    {
        if (IsInitialized) return;

        TilePosition = tilePosition;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y, 1.0f);
        ItemType = itemType;
        TileSpriteRenderer.sprite = sprite;

        IsInitialized = true;
    }

    // TODO: inventory
    public bool Interact(Character character)
    {
        if (character.HasItemKey)
        {
            Destroy();
            return true;
        }

        return false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
