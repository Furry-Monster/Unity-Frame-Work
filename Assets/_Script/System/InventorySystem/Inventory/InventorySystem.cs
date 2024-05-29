using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    public InventorySO inventory { get; private set; }

    private void Start()
    {
        inventory = Resources.Load<InventorySO>("ScriptableObjects/Inventory");
    }
}
