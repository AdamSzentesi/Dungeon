using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct TileProperties
{
    public TileType TileType;
    public ItemType ItemType;
}

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelData levelData = (LevelData)target;

        GUILayout.BeginVertical();
        for (int y = levelData.Height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();
            for (int x = 0; x < levelData.Width; x++)
            {
                TileType oldTileType = levelData.GetTileType(x, y);
                ItemType oldItemType = levelData.GetItemType(x, y);

                GUILayout.BeginVertical();
                TileType newTileType = (TileType)EditorGUILayout.EnumPopup(oldTileType, GUILayout.Width(40), GUILayout.Height(20));
                ItemType newItemType = (ItemType)EditorGUILayout.EnumPopup(oldItemType, GUILayout.Width(40), GUILayout.Height(30));
                GUILayout.EndVertical();

                if (oldTileType != newTileType)
                {
                    levelData.SetTileType(x, y, newTileType);
                }
                if (oldItemType != newItemType)
                {
                    levelData.SetItemType(x, y, newItemType);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

    }
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Dungeon/LevelData")]
public class LevelData : ScriptableObject
{
    public TileProperties[] _TileProperties;

    private int _OldWidth = 0;
    private int _OldHeight = 0;

    public int Width = 10;
    public int Height = 10;

    public void OnValidate()
    {
        if (_TileProperties == null)
        {
            _TileProperties = new TileProperties[Width * Height];
        }

        if (_OldWidth != Width || _OldHeight != Height)
        {
            int newCount = Width * Height;
            int oldCount = _TileProperties.Length;

            TileProperties[] newArray = new TileProperties[newCount];

            if (newCount > oldCount)
            {
                for (int x = 0; x < _OldWidth; x++)
                {
                    for (int y = 0; y < _OldHeight; y++)
                    {
                        newArray[Width * y + x] = _TileProperties[_OldWidth * y + x];
                    }
                }
            }
            else
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        newArray[Width * y + x] = _TileProperties[_OldWidth * y + x];
                    }
                }
            }

            _TileProperties = newArray;
            _OldWidth = Width;
            _OldHeight = Height;
        }
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
