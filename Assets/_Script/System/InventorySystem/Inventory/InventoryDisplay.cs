using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : Singleton<InventoryDisplay>
{
    //ref
    private InventorySystem inventorySystem => Singleton<InventorySystem>.Instance;

    [SerializeField] private Image[] slots = new Image[4];

    protected override void Awake()
    {
        base.Awake();

        if (slots.Length < 4)
        {
            Debug.LogError("InventoryDisplay: Not enough slot in the inventory display");
        }
    }

    private void Start()
    {
        inventorySystem.OnInvenoryChanged += InventoryUpdate;

        inventorySystem.OnSelectedSlotChanged += SelectedSlotUpdate;
    }


    #region Reusable
    private void InventoryUpdate()
    {
        List<ItemSO> currentSlots = inventorySystem.inventory.GetItems();

        //update slot image 
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = currentSlots[i].itemIcon;
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
