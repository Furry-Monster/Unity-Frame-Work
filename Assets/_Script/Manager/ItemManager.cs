using System.Collections.Generic;
using System.Linq;
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
    //itemList(shouldn't be able to visit directly,just for setting in editor)
    [SerializeField] private List<ItemInstance> items = new List<ItemInstance>();
    public List<ItemInstance> Items
    {
        get
        {
            //every time we load some new item,we need to update the items list for visual
            UpdateItemsVisual();
            return items;
        }
    }

    //itemDict
    private Dictionary<string, ItemInstance> itemDict = new Dictionary<string, ItemInstance>();

    //default spawn parent
    [SerializeField] private Transform defaultParent;

    protected override void Awake()
    {
        base.Awake();

        //get default parent
        if (defaultParent == null)
        {
            defaultParent = GameObject.Find("Environment").transform.Find("_Items");
        }
    }

    #region CRUD
    public void AddItem(ItemInstance item)
    {
        if (HasItem(item)) return;

        itemDict.Add(item.basicData.itemName, item);
    }

    public void RemoveItem(string itemID)
    {
        if (HasItem(itemID))
        {
            itemDict.Remove(itemID);
        }
    }

    public void UpdateItem(ItemInstance item)
    {
        if (HasItem(item))
        {
            itemDict[item.basicData.itemName] = item;
        }
    }

    public bool HasItem(string itemID)
    {
        return itemDict.ContainsKey(itemID);
    }
    public bool HasItem(ItemInstance item)
    {
        return itemDict.ContainsKey(item.basicData.itemName);
    }
    public ItemInstance GetItem(string itemID)
    {
        if (itemDict.ContainsKey(itemID))
        {
            return itemDict[itemID];
        }
        return null;
    }
    #endregion

    #region internal
    //init
    internal void Init()
    {
        //load item data from list
        foreach (ItemInstance item in items)
        {
            itemDict.Add(item.basicData.itemName, item);
        }

        Debug.Log($"{itemDict.Count}/{items.Count} items loaded successfully");
    }

    #region Spawn
    internal void SpawnItem(string itemID, Transform parent, Vector3 pos = default, Quaternion rot = default)
    {
        if (itemDict.ContainsKey(itemID))
        {
            ItemInstance item = itemDict[itemID];
            var instance = Instantiate(item.basicData.itemPrefab, parent);

            instance.transform.position = pos;
            instance.transform.rotation = rot;
        }
    }

    internal void SpawnItem(string itemID)
    {
        SpawnItem(itemID, defaultParent);
    }
    #endregion

    #endregion


    #region Editor
    private void UpdateItemsVisual()
    {
        items = itemDict.Values.ToList();
    }

    #endregion
}
