using System;
using UnityEngine;

[Serializable]
public struct TilesetItem
{
    public Sprite Sprite;
}

[CreateAssetMenu(fileName = "Tileset", menuName = "Dungeon/Tileset")]
public class Tileset : ScriptableObject
{
    public Sprite ErrorSprite;

    [SerializeField]
    private TilesetItem[] _TileDataArray = new TilesetItem[Enum.GetNames(typeof(TileType)).Length];

    public Sprite GetSprite(TileType tileType)
    {
        Sprite result = _TileDataArray[(int)tileType].Sprite;

        if (!result) { result = ErrorSprite; }
        
        return result;
    }

}
