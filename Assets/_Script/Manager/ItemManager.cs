using System.Collections.Generic;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要是管理物品的注册和生成，
//          包括物品的注册（CRUD）、生成、销毁、获取等功能。
//==========================

public class ItemManager : Singleton<ItemManager>
{
    //itemList(shouldn't be able to visit directly,just for setting)
    [SerializeField] private List<ItemInstance> items = new List<ItemInstance>();

    //itemDict(provide a way to visit items)
    public Dictionary<int, ItemInstance> itemDict = new Dictionary<int, ItemInstance>();

    //default spawn parent
    [SerializeField] private Transform defaultParent;

    protected override void Awake()
    {
        base.Awake();

        if(defaultParent == null)
        {
            defaultParent = GameObject.Find("Environment").transform.Find("_Items");
        }
    }

    #region CRUD
    public void AddItem(ItemInstance item)
    {
        if (HasItem(item)) return;

        items.Add(item);
        itemDict.Add(item.basicData.itemName.GetHashCode(), item);
    }
    public void AddItems(List<ItemInstance> itemList)
    {
        items.AddRange(itemList);
        foreach (ItemInstance item in itemList)
        {
            if (HasItem(item)) continue;
            itemDict.Add(item.basicData.itemName.GetHashCode(), item);
        }
    }

    public void RemoveItemById(int id)
    {
        if (HasItem(id))
        {
            ItemInstance item = itemDict[id];
            items.Remove(item);
            itemDict.Remove(id);
        }
    }
    public void RemoveItem(ItemInstance item)
    {
        if (HasItem(item))
        {
            items.Remove(item);
            itemDict.Remove(item.basicData.itemName.GetHashCode());
        }
    }

    public void UpdateItem(ItemInstance item)
    {
        if (HasItem(item))
        {
            itemDict[item.basicData.itemName.GetHashCode()] = item;
        }
    }

    public bool HasItem(int id)
    {
        return itemDict.ContainsKey(id);
    }
    public bool HasItem(ItemInstance item)
    {
        return itemDict.ContainsKey(item.basicData.itemName.GetHashCode());
    }
    public ItemInstance GetItemById(int id)
    {
        if (itemDict.ContainsKey(id))
        {
            return itemDict[id];
        }
        return null;
    }
    public List<ItemInstance> GetAllItem()
    {
        return items;
    }
    #endregion

    #region internal
    //init
    internal void Init()
    {
        //load item data from list
        foreach (ItemInstance item in items)
        {
            itemDict.Add(item.basicData.itemName.GetHashCode(), item);
        }

        Debug.Log($"{itemDict.Count}/{items.Count} items loaded successfully");
    }

    #region Spawn
    /// <summary>
    /// To Spawn an item at a specific position in world space
    /// </summary>
    /// <param name="itemID">itemHashID</param>
    /// <param name="position">world space position</param>
    internal void SpawnItemAtPosition(int itemID, Vector3 position)
    {
        if (itemDict.ContainsKey(itemID))
        {
            ItemInstance item = itemDict[itemID];
            Instantiate(item.basicData.itemPrefab, position, Quaternion.identity);
        }
    }

    /// <summary>
    /// To Spawn an item under a specific transform
    /// </summary>
    /// <param name="itemID">itemHashID</param>
    /// <param name="parent">parent transform</param>
    internal void SpawnItemUnderTransform(int itemID, Transform parent)
    {
        if (itemDict.ContainsKey(itemID))
        {
            ItemInstance item = itemDict[itemID];
            var instance = Instantiate(item.basicData.itemPrefab, parent);
        }
    }

    /// <summary>
    /// To Spawn an item under default parent transform
    /// </summary>
    /// <param name="itemID">itemHashID</param>
    internal void SpawnItem(int itemID)
    {
        SpawnItemUnderTransform(itemID, defaultParent);
    }
    #endregion

    #endregion

}
