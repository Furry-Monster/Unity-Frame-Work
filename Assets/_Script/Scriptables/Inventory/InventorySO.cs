using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObjects/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<BaseItemSO> items = new List<BaseItemSO>();
    [SerializeField] private int maxItems = 4;

    #region ReusableMethods
    public bool AddItem(BaseItemSO itemToAdd)
    {
        //if there are empty slots, add the item to the first empty slot
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                return true;
            }
        }

        //if not,try to add push it to the back of the list
        if (items.Count < maxItems)
        {
            items.Add(itemToAdd);
            return true;
        }

        //if the list is full, return false
        Debug.Log("Inventory is full");
        return false;
    }

    public bool AddItem(BaseItemSO itemToAdd, int index)
    {
        if (index < 0 || index >= maxItems)
        {
            Debug.LogWarning("Invalid index");
            return false;
        }

        if (items[index] == null)
        {
            items[index] = itemToAdd;
            return true;
        }

        Debug.Log("Slot is already occupied");
        return false;
    }

    public bool RemoveItem(int index)
    {
        if (items[index] != null)
        {
            items[index] = null;
            return true;
        }

        Debug.Log($"Item not found in slot {index}");
        return false;
    }

    public bool RemoveItem(BaseItemSO itemToRemove)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                return true;
            }
        }

        Debug.Log($"{itemToRemove.name} not found in inventory");
        return false;
    }

    /// <summary>
    /// Better not calling this methods
    /// </summary>
    public void Clear()
    {
        items.Clear();
    }
    #endregion
}
