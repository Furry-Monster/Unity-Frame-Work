using System;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本管理着输入的管理，
//          包括Player输入和UI输入，
//          并且提供了一些事件，用于监听输入的变化。
//          
//          这里我必须阐明这个Manager与其他manager的区别：
//          由于这个Manager属于对InputActions的一个二次封装，所以没有采用字典来做映射，
//          相反我只利用枚举和Switch语句来管理输入的类型，
//          这样的结果是导致输入输出的代码可读性较差
//          同时，由于枚举的限制，我只能定义两种输入类型，
//          这也限制了输入的种类，但对于一般的游戏来说应该够用了。
//==========================
public enum InputType
{
    Player,
    UI
}

public class InputManager : Singleton<InputManager>,ISystem
{
    private InputActions inputActions;

    public InputType currentInputType { get; private set; }

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
    private bool _pauseInput;

    //player input observers
    public event Action OnMove;
    public event Action OnLocomotionToggle;
    public event Action OnInteract;
    public event Action OnDrop;
    public event Action<int> OnSlot;
    public event Action OnPause;

    //player input properties
    public Vector2 moveInput { get => _moveInput; }
    public bool locomotionToggleInput { get => _locomotionToggleInput; }
    public bool interactInput { get => _interactInput; }
    public bool dropInput { get => _dropInput; }
    public bool slot0Input { get => _slot0Input; }
    public bool slot1Input { get => _slot1Input; }
    public bool slot2Input { get => _slot2Input; }
    public bool slot3Input { get => _slot3Input; }
    public bool pauseInput { get => _pauseInput; }
    #endregion

    #region UI input

    //ui cached input
    private bool _clickLeftInput;
    private bool _clickRightInput;
    private bool _exitInput;

    //ui input observers
    public event Action OnClickLeft;
    public event Action OnClickRight;
    public event Action OnExit;

    //ui input properties
    public bool clickLeftInput { get => _clickLeftInput; }
    public bool clickRightInput { get => _clickRightInput; }
    public bool exitInput { get => _exitInput; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        CacheInputs();
    }

    #region Internal Methods
    //init
    public void Init()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();
        }

        Debug.Log("InputManager initialized successfully");
    }

    //ChangeInputType
    internal void ChangeInputType(InputType inputType)
    {
        ClearObservers();

        currentInputType = inputType;

        switch (inputType)
        {
            case InputType.Player:
                inputActions.Player.Enable();

                inputActions.UI.Disable();
                break;
            case InputType.UI:
                inputActions.UI.Enable();

                inputActions.Player.Disable();
                break;
            default:
                Debug.LogError("Invalid input type");
                break;
        }

        InitObserver();
    }
    #endregion

    #region Reusable methods
    private void InitObserver()
    {
        switch (currentInputType)
        {
            case InputType.Player:
                InitPlayerObserver();
                break;
            case InputType.UI:
                InitUIObserver();
                break;
            default:
                Debug.LogError("Invalid input type");
                break;
        }
    }

    private void CacheInputs()
    {
        switch (currentInputType)
        {
            case InputType.Player:
                CachePlayerInputs();
                break;
            case InputType.UI:
                CacheUIInputs();
                break;
            default:
                Debug.LogError("Invalid input type");
                break;
        }
    }

    private void ClearObservers()
    {
        switch (currentInputType)
        {
            case InputType.Player:
                ClearPlayerObserber();
                break;
            case InputType.UI:
                ClearUIObserver();
                break;
            default:
                Debug.LogError("Invalid input type");
                break;
        }
    }
    #endregion

    #region PlayerInput Methods

    private void InitPlayerObserver()
    {
        inputActions.Player.Move.performed += ctx => OnMove?.Invoke();
        inputActions.Player.LocomotionToggle.performed += ctx => OnLocomotionToggle?.Invoke();
        inputActions.Player.Interact.performed += ctx => OnInteract?.Invoke();
        inputActions.Player.Drop.performed += ctx => OnDrop?.Invoke();
        inputActions.Player.Slot0.performed += ctx => OnSlot?.Invoke(0);
        inputActions.Player.Slot1.performed += ctx => OnSlot?.Invoke(1);
        inputActions.Player.Slot2.performed += ctx => OnSlot?.Invoke(2);
        inputActions.Player.Slot3.performed += ctx => OnSlot?.Invoke(3);
        inputActions.Player.Pause.performed += ctx => OnPause?.Invoke();
    }

    private void CachePlayerInputs()
    {
        _moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        _locomotionToggleInput = inputActions.Player.LocomotionToggle.IsPressed();
        _interactInput = inputActions.Player.Interact.IsPressed();
        _dropInput = inputActions.Player.Drop.IsPressed();
        _slot0Input = inputActions.Player.Slot0.IsPressed();
        _slot1Input = inputActions.Player.Slot1.IsPressed();
        _slot2Input = inputActions.Player.Slot2.IsPressed();
        _slot3Input = inputActions.Player.Slot3.IsPressed();
        _pauseInput = inputActions.Player.Pause.IsPressed();
    }

    private void ClearPlayerObserber()
    {
        inputActions.Player.Move.performed -= ctx => OnMove?.Invoke();
        inputActions.Player.LocomotionToggle.performed -= ctx => OnLocomotionToggle?.Invoke();
        inputActions.Player.Interact.performed -= ctx => OnInteract?.Invoke();
        inputActions.Player.Drop.performed -= ctx => OnDrop?.Invoke();
        inputActions.Player.Slot0.performed -= ctx => OnSlot?.Invoke(0);
        inputActions.Player.Slot1.performed -= ctx => OnSlot?.Invoke(1);
        inputActions.Player.Slot2.performed -= ctx => OnSlot?.Invoke(2);
        inputActions.Player.Slot3.performed -= ctx => OnSlot?.Invoke(3);
        inputActions.Player.Pause.performed -= ctx => OnPause?.Invoke();
    }
    #endregion
    #region UIInput Methods
    private void InitUIObserver()
    {
        inputActions.UI.ClickLeft.performed += ctx => OnClickLeft?.Invoke();
        inputActions.UI.ClickRight.performed += ctx => OnClickRight?.Invoke();
        inputActions.UI.Exit.performed += ctx => OnExit?.Invoke();
    }

    private void CacheUIInputs()
    {
        _clickLeftInput = inputActions.UI.ClickLeft.IsPressed();
        _clickRightInput = inputActions.UI.ClickRight.IsPressed();
        _exitInput = inputActions.UI.Exit.IsPressed();
    }

    private void ClearUIObserver()
    {
        inputActions.UI.ClickLeft.performed -= ctx => OnClickLeft?.Invoke();
        inputActions.UI.ClickRight.performed -= ctx => OnClickRight?.Invoke();
        inputActions.UI.Exit.performed -= ctx => OnExit?.Invoke();
    }
    #endregion
}
