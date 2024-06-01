using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private InputActions inputActions;

    #region Player input;

    //player cached input
    private Vector2 _moveInput;
    private bool _locomotionToggleInput;
    private bool _interactInput;
    private bool _dropInput;
    private bool _slot0Input;
    private bool _slot1Input;
    private bool _slot2Input;
    private bool _slot3Input;

    //player input observers
    public event Action OnMove;
    public event Action OnLocomotionToggle;
    public event Action OnInteract;
    public event Action OnDrop;
    public event Action OnSlot0;
    public event Action OnSlot1;
    public event Action OnSlot2;
    public event Action OnSlot3;

    //player input properties
    public Vector2 moveInput { get => _moveInput; }
    public bool locomotionToggleInput { get => _locomotionToggleInput; }
    public bool interactInput { get => _interactInput; }
    public bool dropInput { get => _dropInput; }
    public bool slot0Input { get => _slot0Input; }
    public bool slot1Input { get => _slot1Input; }
    public bool slot2Input { get => _slot2Input; }
    public bool slot3Input { get => _slot3Input; }
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
        inputActions.Player.LocomotionToggle.performed += ctx => OnLocomotionToggle?.Invoke();
        inputActions.Player.Interact.performed += ctx => OnInteract?.Invoke();
        inputActions.Player.Drop.performed += ctx => OnDrop?.Invoke();
        inputActions.Player.Slot0.performed += ctx => OnSlot0?.Invoke();
        inputActions.Player.Slot1.performed += ctx => OnSlot1?.Invoke();
        inputActions.Player.Slot2.performed += ctx => OnSlot2?.Invoke();
        inputActions.Player.Slot3.performed += ctx => OnSlot3?.Invoke();
    }

    private void CacheInputs()
    {
        _moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        _locomotionToggleInput = inputActions.Player.LocomotionToggle.IsPressed();
        _interactInput = inputActions.Player.Interact.IsPressed();
        _dropInput = inputActions.Player.Drop.IsPressed();
        _slot1Input = inputActions.Player.Slot1.IsPressed();
        _slot2Input = inputActions.Player.Slot2.IsPressed();
        _slot3Input = inputActions.Player.Slot3.IsPressed();
        _slot0Input = inputActions.Player.Slot0.IsPressed();
    }

    private void ClearEventObserber()
    {
        inputActions.Player.Move.performed -= ctx => OnMove?.Invoke();
        inputActions.Player.LocomotionToggle.performed -= ctx => OnLocomotionToggle?.Invoke();
        inputActions.Player.Interact.performed -= ctx => OnInteract?.Invoke();
        inputActions.Player.Drop.performed -= ctx => OnDrop?.Invoke();
        inputActions.Player.Slot1.performed -= ctx => OnSlot1?.Invoke();
        inputActions.Player.Slot2.performed -= ctx => OnSlot2?.Invoke();
        inputActions.Player.Slot3.performed -= ctx => OnSlot3?.Invoke();
        inputActions.Player.Slot0.performed -= ctx => OnSlot0?.Invoke();
    }
    #endregion

    #region internal methods

    internal void EnableInput(object sender)
    {
        switch (sender)
        {
            case PlayerUnit:
                inputActions.Player.Enable();
                break;
            default:
                InitEventObserver();
                inputActions.Enable();
                break;
        }
    }

    internal void DisableInput(object sender)
    {
        switch (sender)
        {
            case PlayerUnit:
                inputActions.Player.Disable();
                break;
            default:
                ClearEventObserber();
                inputActions.Disable();
                break;
        }
    }
    #endregion
}
