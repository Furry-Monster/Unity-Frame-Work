using System;
using UnityEngine;

public class PlayerUnit : UnitInstanceContainer
{
    //Data
    internal PlayerUnitSO data
    {
        get
        {
            return unit.unitData as PlayerUnitSO;
        }
    }

    //components
    [SerializeField] public Rigidbody rb;

    //FSMs
    private PlayerStateMachine playerFSM;

    //cache
    protected ItemInstanceContainer currentSelectedItem;

    //events
    public event Action<ItemChangedArgs> OnSelectedItemChanged;
    public class ItemChangedArgs : EventArgs
    {
        public ItemInstanceContainer item;

        public ItemChangedArgs(ItemInstanceContainer item)
        {
            this.item = item;
        }
    }


    #region Main Methods
    protected void Awake()
    {

        Debug.Log("PlayerUnit spawned");

        //set components
        rb = GetComponent<Rigidbody>();

        //init FSMs
        playerFSM = new PlayerStateMachine(this);
    }

    //private void OnEnable() => Singleton<InputManager>.Instance.ChangeInputType(InputType.Player);

    //private void OnDisable() => Singleton<InputManager>.Instance.ChangeInputType(InputType.UI);

    private void Start()
    {
        //kick off the FSM
        playerFSM.ChangeState(playerFSM.states[PlayerState.Idle]);

        //enable player interaction
        Singleton<InputManager>.Instance.OnInteract += InteractWithItem;
    }

    private void Update()
    {
        //firstly check if there is any transision in FSM;
        playerFSM.HandleInput();
        //then update the FSM
        playerFSM.UpdateStateMachine();

        CheckForItem();
    }

    private void FixedUpdate()
    {
        playerFSM.FixedUpdateStateMachine();
    }

    #endregion

    #region Reusable
    private void CheckForItem()
    {
        ItemInstanceContainer _newSelectedItem = null;

        //sphere check
        //note:
        //the sphere origin point is moved backward,so that player can dectect the item close to it
        //magic number 1.6f is used to make the sphere bigger than the actual radius to make it easier to interact with the item
        if (Physics.SphereCast(
            transform.position + Vector3.up * data.height / 2 - transform.forward * data.radius,
            data.radius * 1.6f, transform.forward,
            out RaycastHit hitInfo, data.interactDistance, data.interactLayer
            ))
        {
            if (hitInfo.collider.TryGetComponent(out _newSelectedItem) == true)
            {
                currentSelectedItem = _newSelectedItem;
            }
            else
            {
                currentSelectedItem = null;
            }
        }
        else
        {
            currentSelectedItem = null;
        }

        //told every item that they should check if it's needed to change the material
        OnSelectedItemChanged?.Invoke(new ItemChangedArgs(currentSelectedItem));
    }
    private void InteractWithItem()
    {
        if (currentSelectedItem != null)
        {
            currentSelectedItem.Pick();
        }
        else
        {
            Debug.Log("No item selected");
        }
    }

    #endregion

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {

        //check sphere gizmos
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * data.interactDistance);
    }
#endif
}
