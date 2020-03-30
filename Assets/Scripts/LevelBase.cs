using UnityEngine;

public abstract class LevelBase : MonoBehaviour
{
    public abstract Sprite GetSprite(TileType tileType);
    public abstract void Dispatch(TileEvent tileEvent, Vector2Int desiredTilePosition, Character character);
}
