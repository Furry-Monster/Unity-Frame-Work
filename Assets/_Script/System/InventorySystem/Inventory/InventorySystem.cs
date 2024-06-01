using System;
using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    public InventorySO inventory { get; private set; }

    [SerializeField] private string PATH = "ScriptableObjects/Inventory";

    //events
    public event Action OnInvenoryChanged;


    protected override void Awake()
    {
        base.Awake();

        inventory = Resources.Load<InventorySO>(PATH);
        Debug.Log("inventory data loaded successfully!");
    }

    #region internal
    internal void AddItem(ItemSO item)
    {
        if (inventory.AddItem(item))
        {
            OnInvenoryChanged?.Invoke();
        }
    }

    internal void AddItem(ItemSO item, int slotIndex)
    {
        if (inventory.AddItem(item, slotIndex))
        {
            OnInvenoryChanged?.Invoke();
        }
    }
    
    internal void RemoveItem(int slotIndex)
    {
        if (inventory.RemoveItem(slotIndex))
        {
            OnInvenoryChanged?.Invoke();
        }
    }

    internal void RemoveItem(ItemSO item)
    {
        if (inventory.RemoveItem(item))
        {
            OnInvenoryChanged?.Invoke();
        }
    }
    #endregion
}
