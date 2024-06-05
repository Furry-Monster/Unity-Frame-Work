using System;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这里管理游戏的状态机，
//          包括游戏开始，初始化，菜单，加载，游戏中，暂停等状态。
//          
//          对于游戏状态的划分，我没有很多经验，
//          所以这里的划分可能比较粗糙，需要根据实际情况进行调整。
//==========================

/// <summary>
/// States that your game might be in
/// </summary>
[Serializable]
public enum GameState
{
    Starting,
    Initializing, //please distinguish between loading and initializing,this is for init systems,only load once.
    Menu,
    Loading,// this is for loading assets, could load for many times
    GamePlay,
    Paused,
}

public class GameManager : Singleton<GameManager>
{
    //events for state changing
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    //current game state
    public GameState currentState { get; private set; }

    //kick of the game state machine
    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
#pragma warning disable CS0472 // 由于此类型的值永不等于 "null"，该表达式的结果始终相同
        if (currentState != null)
        {
            OnAfterStateChanged?.Invoke(currentState);
        }
#pragma warning restore CS0472 // 由于此类型的值永不等于 "null"，该表达式的结果始终相同
        OnBeforeStateChanged?.Invoke(newState);

        currentState = newState;

        switch (currentState)
        {
            case GameState.Starting:
                HandleStartingEnter();
                break;
            case GameState.Initializing:
                HandleInitializingEnter();
                break;
            case GameState.Menu:
                HandleMenuEnter();
                break;
            case GameState.Loading:
                HandleLoadingEnter();
                break;
            case GameState.GamePlay:
                HandleGamePlayEnter();
                break;
            case GameState.Paused:
                HandlePausedEnter();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    //Handlers for state entering
    private void HandleStartingEnter()
    {
        //don't know what should be done here......

        ChangeState(Singleton<Systems>.Instance.isInit ? GameState.Loading : GameState.Initializing);
    }
    private void HandleInitializingEnter()
    {
        //system init,init data,set references,etc.
        InitAll();

        Singleton<Systems>.Instance.isInit = true;
        ChangeState(GameState.Menu);
    }
    private void HandleMenuEnter()
    {
        //just for debug,change to loading after 2 seconds
        Singleton<TimerSystem>.Instance.Schedule(ctx => ChangeState(GameState.Loading), 2f);
    }
    private void HandleLoadingEnter()
    {
        //Spawn player
        Singleton<UnitManager>.Instance.SpawnUnit(Singleton<UnitManager>.Instance.GetUnitById("Player".GetHashCode())); Singleton<InputManager>.Instance.ChangeInputType(InputType.Player);
        //Enable input mapping
        Singleton<InputManager>.Instance.ChangeInputType(InputType.Player);
        //Spawn test item
        Singleton<ItemManager>.Instance.SpawnItem("Stick".GetHashCode());

        ChangeState(GameState.GamePlay);
    }
    private void HandleGamePlayEnter()
    {

    }
    private void HandlePausedEnter()
    {

    }

    #region Reusable
    private void InitAll()
    {
        Singleton<UnitManager>.Instance.Init();
        Singleton<ItemManager>.Instance.Init();
        Singleton<UIManager>.Instance.Init();
        Singleton<InputManager>.Instance.Init();
        Singleton<AssetBundleManager>.Instance.Init();
    }

    #endregion


}
