using UnityEngine;

public class PlayerUnit : BaseUnit
{
    //Data
    internal PlayerUnitSO data
    {
        get
        {
            return unitData as PlayerUnitSO;
        }
    }

    //components
    [SerializeField] public Rigidbody rb;

    //FSMs
    private PlayerStateMachine playerFSM;


    #region Main Methods
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("PlayerUnit spawned");

        //set components
        rb = GetComponent<Rigidbody>();

        //init FSMs
        playerFSM = new PlayerStateMachine(this);
    }

    private void OnEnable() => Singleton<InputManager>.Instance?.EnableInput(this);

    private void OnDisable() => Singleton<InputManager>.Instance?.DisableInput(this);

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
