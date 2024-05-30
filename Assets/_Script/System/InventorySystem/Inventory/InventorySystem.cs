using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    public InventorySO inventory { get; private set; }

    [SerializeField] private string PATH = "ScriptableObjects/Inventory";

    protected override void Awake()
    {
        base.Awake();

        inventory = Resources.Load<InventorySO>(PATH);
        Debug.Log("inventory data loaded successfully!");
    }


}
