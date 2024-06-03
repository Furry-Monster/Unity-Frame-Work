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

    #region Main Methods
    protected void Awake()
    {

        Debug.Log("PlayerUnit spawned");

        //set components
        rb = GetComponent<Rigidbody>();

        //init FSMs
        playerFSM = new PlayerStateMachine(this);
    }

    private void OnEnable() => Singleton<InputManager>.Instance.ChangeInputType(InputType.Player);

    private void OnDisable() => Singleton<InputManager>.Instance.ChangeInputType(InputType.UI);

    private void Start()
    {
        //kick off the FSM
        playerFSM.ChangeState(playerFSM.states[PlayerState.Idle]);
    }

    private void Update()
    {
        //firstly check if there is any transision in FSM;
        playerFSM.HandleInput();

        //then update the FSM
        playerFSM.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        playerFSM.FixedUpdateStateMachine();
    }

    #endregion
}
