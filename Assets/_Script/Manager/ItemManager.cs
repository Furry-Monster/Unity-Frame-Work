using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<string, BaseItem> itemDict = new Dictionary<string, BaseItem>();

    #region SpawnItem
    internal void SpawnItemAtPosition(string itemName, Vector3 position)
    {
        if (itemDict.ContainsKey(itemName))
        {
            BaseItem item = itemDict[itemName];
            Instantiate(item.basicData.itemPrefab, position, Quaternion.identity);
        }
    }
    #endregion
    #region ClearItem
    internal void ClearItem()
    {

    }
    #endregion

    #region ReusableMethods

    public void AddItem(BaseItem item)
    {
        if (itemDict.ContainsKey(item.basicData.itemName))
        {
            Debug.LogWarning("Item already exists in the dictionary");
        }
        else
        {
            itemDict.Add(item.basicData.itemName, item);
        }
    }

    public void RemoveItem(string itemName)
    {
        if (itemDict.ContainsKey(itemName))
        {
            itemDict.Remove(itemName);
        }
        else
        {
            Debug.LogWarning("Item does not exist in the dictionary");
        }
    }

    public bool HasItem(string itemName)
    {
        return itemDict.ContainsKey(itemName);
    }

    public BaseItem GetItem(string itemName)
    {
        if (itemDict.ContainsKey(itemName))
        {
            return itemDict[itemName];
        }
        else
        {
            Debug.LogWarning("Item does not exist in the dictionary");
            return null;
        }
    }

    public List<BaseItem> GetAllItems()
    {
        return new List<BaseItem>(itemDict.Values);
    }


    #endregion
}
