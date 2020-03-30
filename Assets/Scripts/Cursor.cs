using UnityEngine;

public class Cursor : Tileable
{
    public void SetTilePosition(Vector2Int desiredTilePosition)
    {
        TilePosition = desiredTilePosition;
        transform.localPosition = new Vector3(TilePosition.x, TilePosition.y);
    }

}
