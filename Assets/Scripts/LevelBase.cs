using UnityEngine;

public abstract class LevelBase : MonoBehaviour
{
    public abstract Sprite GetSprite(TileType tileType);
    
}
