using System;
using UnityEngine;

[Serializable]
public struct TileProperties
{
    public TileType TileType;
    public ItemType ItemType;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Dungeon/LevelData")]
public class LevelData : ScriptableObject
{
    public int Width { get; private set; } = 10;
    public int Height { get; private set; } = 10;
    public Vector2Int StartLocation = new Vector2Int();

    public TileProperties[] _TileProperties;
    public Tileset Tileset { get; private set; }

    public void Setup(int width, int height, TileProperties[] tilePropertiesArray, Tileset tileset)
    {
        Width = width;
        Height = height;
        _TileProperties = tilePropertiesArray;
        Tileset = tileset;
    }

    // TODO: check bounds
    public void SetTileType(int x, int y, TileType tileType)
    {
        _TileProperties[Width * y + x].TileType = tileType;
    }

    // TODO: check bounds
    public TileType GetTileType(int x, int y)
    {
        return _TileProperties[Width * y + x].TileType;
    }

    // TODO: check bounds
    public void SetItemType(int x, int y, ItemType tileType)
    {
        _TileProperties[Width * y + x].ItemType = tileType;
    }

    // TODO: check bounds
    public ItemType GetItemType(int x, int y)
    {
        return _TileProperties[Width * y + x].ItemType;
    }

}
