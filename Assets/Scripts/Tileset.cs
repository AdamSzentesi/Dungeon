using System;
using UnityEngine;

[Serializable]
public struct TileData
{
    public Sprite Sprite;
    public bool IsWall;
}

[CreateAssetMenu(fileName = "Tileset", menuName = "Dungeon/Tileset")]
public class Tileset : ScriptableObject
{
    public Sprite ErrorSprite;

    [SerializeField]
    private TileData[] _TileDataArray = new TileData[Enum.GetNames(typeof(TileType)).Length];

    public Sprite GetSprite(TileType tileType)
    {
        Sprite result = _TileDataArray[(int)tileType].Sprite;

        if (!result) { result = ErrorSprite; }
        
        return result;
    }

    public bool IsWall(TileType tileType)
    {
        return _TileDataArray[(int)tileType].IsWall;
    }

}
