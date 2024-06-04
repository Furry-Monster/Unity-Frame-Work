using System;
using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    //model data
    public InventorySO inventory { get; private set; }
    [SerializeField] private string PATH = "ScriptableObjects/Inventory";//load path
    //ref to visual
    private InventoryDisplay inventoryDisplay => Singleton<InventoryDisplay>.Instance;

    //events
    public event Action OnInvenoryChanged;//called when the inventory is changed(eg. add or remove item)

    public event Action<int> OnSelectedSlotChanged;//called when select a new slot

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    #region internal
    //init
    internal void Init()
    {
        //init controller
        inventory = Resources.Load<InventorySO>(PATH);
        Debug.Log("inventory data loaded successfully!");

        //init visual will be automatically done by the class itself

        Singleton<InputManager>.Instance.OnSlot += ctx => OnSelectedSlotChanged?.Invoke(ctx);
    }

    //add
    internal void AddItem(ItemInstance item)
    {
        if (inventory.AddItem(item))
        {
            OnInvenoryChanged?.Invoke();
        }
    }
    internal void AddItem(ItemInstance item, int slotIndex)
    {
        if (inventory.AddItem(item, slotIndex))
        {
            OnInvenoryChanged?.Invoke();
        }
    }

    //remove
    internal void RemoveItem(int slotIndex)
    {
        if (inventory.RemoveItem(slotIndex))
        {
            OnInvenoryChanged?.Invoke();
        }
    }
    internal void RemoveItem(ItemInstance item)
    {
        if (inventory.RemoveItem(item))
        {
            OnInvenoryChanged?.Invoke();
        }
    }

    #endregion
}
