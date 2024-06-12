using System;
using UnityEngine;


//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本是基于Item系统开发的 库存系统，
//          由于是在Item系统上进行迭代，可读性较差，未来会进行重构
//==========================
public class InventorySystem : Singleton<InventorySystem>,ISystem
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

        
    }

    #region internal
    //init
    public void Init()
    {
        //init controller
        inventory = Resources.Load<InventorySO>(PATH);
        Debug.Log("inventory data loaded successfully!");

        //init visual will be automatically done by the class itself

        Singleton<InputManager>.Instance.OnSlot += ctx => OnSelectedSlotChanged?.Invoke(ctx);
        Debug.Log("InventorySystem inited!");

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
