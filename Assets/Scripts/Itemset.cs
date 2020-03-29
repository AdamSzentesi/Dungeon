using System;
using UnityEngine;

[Serializable]
public struct ItemData
{
    public Sprite Sprite;
    public bool IsPickup;
    public bool IsBlocking;
}

[CreateAssetMenu(fileName = "Itemset", menuName = "Dungeon/Itemset")]
public class Itemset : ScriptableObject
{
    public Sprite ErrorSprite;

    [SerializeField]
    private ItemData[] _ItemDataArray = new ItemData[Enum.GetNames(typeof(ItemType)).Length];

    public Sprite GetSprite(ItemType itemType)
    {
        Sprite result = _ItemDataArray[(int)itemType].Sprite;

        if (!result) { result = ErrorSprite; }

        return result;
    }

    public bool IsPickup(ItemType itemType)
    {
        return _ItemDataArray[(int)itemType].IsPickup;
    }

    public bool IsBlocking(ItemType itemType)
    {
        return _ItemDataArray[(int)itemType].IsBlocking;
    }

}
