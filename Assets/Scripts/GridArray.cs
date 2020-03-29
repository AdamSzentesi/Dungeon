using System;
using UnityEngine;

[Serializable]
public class GridArray
{
    public LevelEditor Owner;

    public Tile[] _Tiles;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public GridArray(int width, int height)
    {
        if (width < 1) width = 1;
        if (height < 1) height = 1;

        _Tiles = new Tile[width * height];
    }

    public GridArray() : this(1, 1) { }

    public void SetSize(int width, int height, bool manageData)
    {
        SetColumnCount(width, manageData);
        SetRowCount(height, manageData);
    }

    public void SetColumnCount(int newWidth, bool manageData)
    {
        int change = newWidth - Width;

        if (change == 0) return;
        if (newWidth < 1) return;

        Tile[] newItems = new Tile[newWidth * Height];
        int copyCount = Mathf.Min(newWidth, Width);

        if (manageData)
        {
            // Copy valid tiles
            for (int x = 0; x < copyCount; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    newItems[newWidth * y + x] = GetTile(x, y);
                }
            }

            // Remove old tiles
            for (int x = copyCount; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Owner.DestroyTile(x, y);
                }
            }
        }

        _Tiles = newItems;
        Width = newWidth;

        if (manageData)
        {
            // Add new tiles
            for (int x = copyCount; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Owner.CreateTile(new Vector2Int(x, y));
                }
            }
        }
    }

    public void SetRowCount(int newHeight, bool manageData)
    {
        int change = newHeight - Height;

        if (change == 0) return;
        if (newHeight < 1) return;

        Tile[] newItems = new Tile[Width * newHeight];
        int copyCount = Mathf.Min(newHeight, Height);

        if (manageData)
        {
            // Copy valid tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < copyCount; y++)
                {
                    newItems[Width * y + x] = GetTile(x, y);
                }
            }

            // Remove old tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = copyCount; y < Height; y++)
                {
                    Owner.DestroyTile(x, y);
                }
            }
        }

        _Tiles = newItems;
        Height = newHeight;

        if (manageData)
        {
            // Add new tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = copyCount; y < Height; y++)
                {
                    Owner.CreateTile(new Vector2Int(x, y));
                }
            }
        }
    }

    // TODO: check bounds
    public Tile GetTile(int x, int y)
    {
        return _Tiles[Index(x, y)];
    }

    // TODO: check bounds
    public void SetTile(int x, int y, Tile item)
    {
        _Tiles[Index(x, y)] = item;
    }

    public void Clean()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                _Tiles[Index(x, y)] = default;
            }
        }
    }

    public void Clean(int width, int height)
    {
        Clean();
        SetSize(width, height, false);
    }

    public void DebugPrint()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Debug.Log("- Item " + x + ", " + y + ": " + GetTile(x, y));
            }
        }
    }

    // TODO: check bounds
    private int Index(int x, int y)
    {
        return Width * y + x;
    }

}
