using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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

        if(slots.Length < 4)
        {
            Debug.LogError("InventoryDisplay: Not enough slot in the inventory display");
        }
    }

    private void Start()
    {
        inventorySystem.OnInvenoryChanged += InventoryUpdate;
    }

    private void InventoryUpdate()
    {
        List<ItemSO> currentSlots = inventorySystem.inventory.GetItems();

        //update slot image 
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = currentSlots[i].itemIcon;
        }
    }
}
