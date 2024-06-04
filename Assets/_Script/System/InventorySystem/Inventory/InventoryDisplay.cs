using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : Singleton<InventoryDisplay>
{
    //model data
    public InventorySO inventory { get; private set; }
    [SerializeField] private string PATH = "ScriptableObjects/Inventory";//load path
    //ref to controller
    private InventorySystem inventorySystem => Singleton<InventorySystem>.Instance;

    [SerializeField] private Image[] slots = new Image[4];

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (inventory == null)
        {
            inventory = Resources.Load<InventorySO>(PATH);
        }

        inventorySystem.OnInvenoryChanged += InventoryUpdate;

        inventorySystem.OnSelectedSlotChanged += SelectedSlotUpdate;
    }

    #region Reusable
    private void InventoryUpdate()
    {
        List<ItemInstance> currentSlots = inventory.GetItems();

        //update slot image 
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = currentSlots[i].basicData.itemIcon;
        }
    }

    private void SelectedSlotUpdate(int slotIndex)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == slotIndex)
            {
                slots[i].color = Color.green;
            }
            else
            {
                slots[i].color = Color.white;
            }
        }

        Debug.Log("Selected slot: " + slotIndex);
    }

    #endregion
}
