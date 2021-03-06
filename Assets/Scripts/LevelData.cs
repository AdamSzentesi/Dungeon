﻿using System;
using UnityEditor;
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
    public int Width = 10;
    public int Height = 10;
    public TileProperties[] TileProperties;
    public Tileset Tileset;
    public Vector2Int EntranceTilePosition;
    public Vector2Int ExitTilePosition;

    private void Awake()
    {
        if (TileProperties == null)
        {
            TileProperties = new TileProperties[Width * Height];
        }
    }

    public void StoreData(int width, int height, TileProperties[] tilePropertiesArray, Tileset tileset, Vector2Int entranceTilePosition, Vector2Int exitTilePosition)
    {
        Width = width;
        Height = height;
        Tileset = tileset;
        EntranceTilePosition = entranceTilePosition;
        ExitTilePosition = exitTilePosition;

        TileProperties = tilePropertiesArray;

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    // TODO: check bounds
    public void SetTileType(int x, int y, TileType tileType)
    {
        TileProperties[Width * y + x].TileType = tileType;
    }

    // TODO: check bounds
    public TileType GetTileType(int x, int y)
    {
        int index = Width * y + x;
        return TileProperties[index].TileType;
    }

    // TODO: check bounds
    public void SetItemType(int x, int y, ItemType tileType)
    {
        TileProperties[Width * y + x].ItemType = tileType;
    }

    // TODO: check bounds
    public ItemType GetItemType(int x, int y)
    {
        return TileProperties[Width * y + x].ItemType;
    }

}
