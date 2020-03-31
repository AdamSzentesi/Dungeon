using UnityEngine;

public class Level : LevelBase
{
    public LevelData LevelData;
    public GameObject TilePrefab;
    public GameObject ItemPrefab;
    public Itemset Itemset;
    public Hero Hero;
    public CameraController CameraController;
    public GameObject GameOverScreen;
    public Cursor Cursor;

    public static Level Instance { get; private set; }

    private Tile[,] _Tiles;
    private Item[,] _Items;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        if (!TilePrefab) return;

        Tile prefabTile = TilePrefab.GetComponent<Tile>();
        if (!prefabTile) return;

        TileBehaviorDatabase.Init();

        _Tiles = new Tile[LevelData.Width, LevelData.Height];
        _Items = new Item[LevelData.Width, LevelData.Height];

        for (int x = 0; x < LevelData.Width; x++)
        {
            for (int y = 0; y < LevelData.Height; y++)
            {
                if (!_Tiles[x, y])
                {
                    TileType tileType = LevelData.GetTileType(x, y);
                    GameObject newTile = Instantiate(TilePrefab);
                    _Tiles[x, y] = newTile.GetComponent<Tile>();
                    _Tiles[x, y].Init(new Vector2Int(x, y), tileType, transform, this);
                }

                ItemType itemType = LevelData.GetItemType(x, y);
                if (itemType != ItemType.None)
                {
                    GameObject newItem = Instantiate(ItemPrefab);
                    _Items[x, y] = newItem.GetComponent<Item>();
                    _Items[x, y].Init(new Vector2Int(x, y), itemType, transform, Itemset.GetSprite(itemType));
                }
            }
        }

        Hero.SetTilePosition(LevelData.StartLocation);

        CameraController.SetLocalPosition(new Vector3(LevelData.Width / 2.0f - 0.5f, LevelData.Height / 2.0f - 0.5f));
    }

    private void Update()
    {
        Vector2Int tilePosition = ComputeCursorTilePosition;

        if (IsTilePositionValid(tilePosition))
        {
            Cursor.SetTilePosition(tilePosition);
        }

        if (InputManager.GetActionUp(InputAction.ChaneTileType))
        {
            Tile tile = GetTile(Cursor.TilePosition);
            if (tile)
            {
                tile.SetTileType(TileType.Wall);
            }
        }
    }

    private Tile GetTile(Vector2Int tilePosition)
    {
        if (IsTilePositionValid(tilePosition))
        {
            return _Tiles[tilePosition.x, tilePosition.y];
        }

        return null;
    }

    private Vector2Int ComputeCursorTilePosition
    {
        get
        {
            Vector3 worldPosition = CameraController.Camera.ScreenToWorldPoint(InputManager.MouseScreenPosition);
            worldPosition -= transform.position;
            return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
        }
    }

    public Item PickupItem(Vector2Int tilePosition)
    {
        ItemType itemType = LevelData.GetItemType(tilePosition.x, tilePosition.y);

        Item item = _Items[tilePosition.x, tilePosition.y];

        if (item && Itemset.IsPickup(itemType))
        {
            item.Destroy();
            return item;
        }

        return null;
    }

    public bool IsTilePositionValid(Vector2Int tilePosition)
    {
        bool isOutsideLevel =
        (
            tilePosition.x < 0
            || tilePosition.x >= LevelData.Width
            || tilePosition.y < 0
            || tilePosition.y >= LevelData.Height
        );

        return !isOutsideLevel;
    }

    public bool IsTilePositionValidFloor(Vector2Int tilePosition)
    {
        if (!IsTilePositionValid(tilePosition)) return false;
        if (LevelData.Tileset.IsWall(_Tiles[tilePosition.x, tilePosition.y].TileType)) return false;

        return true;
    }

    public Item GetBlockingItem(Vector2Int tilePosition)
    {
        Item item = _Items[tilePosition.x, tilePosition.y];

        if (item && Itemset.IsBlocking(item.ItemType))
        {
            return item;    
        }

        return null;
    }

    public override Sprite GetSprite(TileType tileType)
    {
        return LevelData.Tileset.GetSprite(tileType);
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }

    // TileEvent

    public override void Dispatch(TileEvent tileEvent, Vector2Int desiredTilePosition, Character character)
    {
        _Tiles[desiredTilePosition.x, desiredTilePosition.y].OnTileEvent(tileEvent, character);
    }

}
