using System;
using UnityEngine;
using UnityEngine.Windows;

public class InventorySystem : Singleton<InventorySystem>
{
    public InventorySO inventory { get; private set; }

    [SerializeField] private string PATH = "ScriptableObjects/Inventory";

    //events
    public event Action OnInvenoryChanged;//called when the inventory is changed(eg. add or remove item)

    public event Action<int> OnSelectedSlotChanged;//called when select a new slot

    protected override void Awake()
    {
        base.Awake();

        inventory = Resources.Load<InventorySO>(PATH);
        Debug.Log("inventory data loaded successfully!");

        AddListeners();
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

    #region Reusable
    private void AddListeners()
    {
        Singleton<InputManager>.Instance.OnSlot0 += OnSlotChanged;
        Singleton<InputManager>.Instance.OnSlot1 += OnSlotChanged;
        Singleton<InputManager>.Instance.OnSlot2 += OnSlotChanged;
        Singleton<InputManager>.Instance.OnSlot3 += OnSlotChanged;
    }

    private void OnSlotChanged(int slotIndex)
    {
        OnSelectedSlotChanged?.Invoke(slotIndex);
    }
    #endregion
}
