using UnityEngine;

public class ItemInstanceContainer : MonoBehaviour
{
    //abstract instance
    public ItemInstance itemInstance;

    //ref
    private PlayerUnit player;
    private Renderer itemRenderer;

    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnit>();
        itemRenderer = GetComponent<Renderer>();

        player.OnSelectedItemChanged += HighlightSelectedItem;
    }


    #region reusable
    private void HighlightSelectedItem(PlayerUnit.ItemChangedArgs args)
    {
        if (args.item == this)
        {
            // highlight the item
            itemRenderer.material = highlightMaterial;

            Debug.Log($"{itemInstance.basicData.itemName} is selected!");
        }
        else
        {
            // unhighlight the item
            itemRenderer.material = defaultMaterial;
        }
    }

    public void Pick()
    {

    }
    #endregion
}
