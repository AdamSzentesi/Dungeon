using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelEditor script = (LevelEditor)target;

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        if (GUILayout.Button("+", GUILayout.Width(40), GUILayout.Height(20)))
        {
            script.AddRow();
        }
        if (GUILayout.Button("-", GUILayout.Width(40), GUILayout.Height(20)))
        {
            script.RemoveRow();
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        if (GUILayout.Button("Save", GUILayout.Width(40), GUILayout.Height(40)))
        {
            script.Save();
        }
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(40);
        if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(40)))
        {
            script.RemoveColumn();
        }
        if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(40)))
        {
            script.AddColumn();
        }
        GUILayout.EndHorizontal();

        DrawDefaultInspector();
    }
}

public class LevelEditor : LevelBase
{
    private LevelData LastLevelData;
    public LevelData LevelData;

    public GameObject TilePrefab;
    public Tileset Tileset;
    public GridArray _TileGridArray = new GridArray();
    
    private void OnValidate()
    {
        if (!TilePrefab) return;
        if (!TilePrefab.GetComponent<Tile>()) return;
        if (!Tileset) return;

        //Debug.Log(LevelData + " vs " + LastLevelData);

        if (LevelData != LastLevelData)
        {
            for (int x = 0; x < _TileGridArray.Width; x++)
            {
                for (int y = 0; y < _TileGridArray.Height; y++)
                {
                    DestroyTile(x, y);
                }
            }

            LastLevelData = LevelData;

            if (!LevelData)
            {
                _TileGridArray.Clean(1, 1);
                return;
            }

            _TileGridArray.Clean(LevelData.Width, LevelData.Height);

            for (int x = 0; x < LevelData.Width; x++)
            {
                for (int y = 0; y < LevelData.Height; y++)
                {
                    if (!_TileGridArray.GetTile(x, y))
                    {
                        TileType tileType = LevelData.GetTileType(x, y);
                        CreateTile(new Vector2Int(x, y), tileType);
                    }
                }
            }

            //_Tiles.DebugPrint();
        }
    }

    public override Sprite GetSprite(TileType tileType)
    {
        return Tileset.GetSprite(tileType);
    }

    public void AddColumn()
    {
        _TileGridArray.SetColumnCount(_TileGridArray.Width + 1, true);
    }

    public void RemoveColumn()
    {
        _TileGridArray.SetColumnCount(_TileGridArray.Width - 1, true);
    }

    public void AddRow()
    {
        _TileGridArray.SetRowCount(_TileGridArray.Height + 1, true);
    }

    public void RemoveRow()
    {
        _TileGridArray.SetRowCount(_TileGridArray.Height - 1, true);
    }

    public void Save()
    {
        if (!LevelData) return;

        TileProperties[] tilePropertiesArray = new TileProperties[_TileGridArray.Width * _TileGridArray.Height];
        for(int i = 0; i < _TileGridArray._Tiles.Length; i++)
        {
            tilePropertiesArray[i].TileType = _TileGridArray._Tiles[i].TileType;
        }

        LevelData.StoreData(_TileGridArray.Width, _TileGridArray.Height, tilePropertiesArray, Tileset);
    }

    public void CreateTile(Vector2Int tilePosition, TileType tileType = TileType.Floor)
    {
        GameObject newTileGO = Instantiate(TilePrefab);
        Tile newTile = newTileGO.GetComponent<Tile>();
        newTile.Init(tilePosition, tileType, transform, this);
        _TileGridArray.SetTile(tilePosition.x, tilePosition.y, newTile);
    }

    public void DestroyTile(int x, int y)
    {
        Tile tile = _TileGridArray.GetTile(x, y);
        if (tile)
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                DestroyImmediate(tile.gameObject);
            };
        }
    }

}
