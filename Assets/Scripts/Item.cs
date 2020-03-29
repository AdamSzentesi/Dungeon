using UnityEngine;

public enum ItemType
{
    None,
    Key,
    Lock,
}

public class Item : MonoBehaviour
{
    public ItemType ItemType;
    public Vector2Int TilePosition = new Vector2Int();
    public bool IsInitialized { get; private set; } = false;

    private SpriteRenderer _SpriteRenderer;

    private void Awake()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2Int tilePosition, ItemType itemType, Transform parentTransform, Sprite sprite)
    {
        if (IsInitialized) return;

        TilePosition = tilePosition;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y, 1.0f);
        ItemType = itemType;
        _SpriteRenderer.sprite = sprite;

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
