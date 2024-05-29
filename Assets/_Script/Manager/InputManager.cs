using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private InputActions inputActions;

    #region Player input;

    //player cached input
    private Vector2 _moveInput;
    private bool _interactInput;
    private bool _dropInput;
    private bool _slot1Input;
    private bool _slot2Input;
    private bool _slot3Input;
    private bool _slot4Input;

    //player input observers
    public event Action OnMove;
    public event Action OnInteract;
    public event Action OnDrop;
    public event Action OnSlot1;
    public event Action OnSlot2;
    public event Action OnSlot3;
    public event Action OnSlot4;

    //player input properties
    public Vector2 moveInput { get => _moveInput; }
    public bool interactInput { get => _interactInput; }
    public bool dropInput { get => _dropInput; }
    public bool slot1Input { get => _slot1Input; }
    public bool slot2Input { get => _slot2Input; }
    public bool slot3Input { get => _slot3Input; }
    public bool slot4Input { get => _slot4Input; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        if (inputActions == null)
        {
            inputActions = new InputActions();
        }

    }

    private void Start()
    {
        InitEventObserver();
    }

    private void Update()
    {
        CacheInputs();
    }

    #region MainMethods

    private void InitEventObserver()
    {
        inputActions.Player.Move.performed += ctx => OnMove?.Invoke();
        inputActions.Player.Interact.performed += ctx => OnInteract?.Invoke();
        inputActions.Player.Drop.performed += ctx => OnDrop?.Invoke();
        inputActions.Player.Slot1.performed += ctx => OnSlot1?.Invoke();
        inputActions.Player.Slot2.performed += ctx => OnSlot2?.Invoke();
        inputActions.Player.Slot3.performed += ctx => OnSlot3?.Invoke();
        inputActions.Player.Slot4.performed += ctx => OnSlot4?.Invoke();
    }

    private void CacheInputs()
    {
        _moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        _interactInput = inputActions.Player.Interact.ReadValue<bool>();
        _dropInput = inputActions.Player.Drop.ReadValue<bool>();
        _slot1Input = inputActions.Player.Slot1.ReadValue<bool>();
        _slot2Input = inputActions.Player.Slot2.ReadValue<bool>();
        _slot3Input = inputActions.Player.Slot3.ReadValue<bool>();
        _slot4Input = inputActions.Player.Slot4.ReadValue<bool>();
    }

    #endregion

    #region internal methods

    internal void EnableInput(object sender)
    {
        switch (sender)
        {
            case Player:
                inputActions.Player.Enable();
                break;
            default:
                inputActions.Enable();
                break;
        }
    }

    internal void DisableInput(object sender)
    {
        switch (sender)
        {
            case Player:
                inputActions.Player.Disable();
                break;
            default:
                inputActions.Disable();
                break;
        }
    }
    #endregion
}
