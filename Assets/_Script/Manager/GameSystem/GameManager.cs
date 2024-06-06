using System;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          ���������Ϸ��״̬����
//          ������Ϸ��ʼ����ʼ�������أ���Ϸ�У���ͣ,���õ�״̬��
//          
//          ������Ϸ״̬�Ļ��֣���û�кܶྭ�飬
//          ��������Ļ��ֿ��ܱȽϴֲڣ���Ҫ����ʵ��������е�����
//==========================

/// <summary>
/// States that your game might be in
/// </summary>
[Serializable]
public enum GameState
{
    Starting,
    Initializing, //please distinguish between loading and initializing,this is for init systems,only load once.
    Loading,      // this is for loading assets, could load for many times
    GamePlay,
    Paused,
    Reset
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
#pragma warning disable CS0472 // ���ڴ����͵�ֵ�������� "null"���ñ��ʽ�Ľ��ʼ����ͬ
        if (currentState != null)
        {
            OnAfterStateChanged?.Invoke(currentState);
        }
#pragma warning restore CS0472 // ���ڴ����͵�ֵ�������� "null"���ñ��ʽ�Ľ��ʼ����ͬ
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
            case GameState.Loading:
                HandleLoadingEnter();
                break;
            case GameState.GamePlay:
                HandleGamePlayEnter();
                break;
            case GameState.Paused:
                HandlePausedEnter();
                break;
            case GameState.Reset:
                HandleResetEnter();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    #region Handlers for state entering
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
        ChangeState(GameState.Loading);
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
    private void HandleResetEnter()
    {


        ChangeState(GameState.Loading);
    }

    #endregion


    #region Reusable
    private void InitAll()
    {
        //critically important managers(global) should be initialized first
        Singleton<GameSceneManager>.Instance.Init();
        Singleton<AssetBundleManager>.Instance.Init();
        Singleton<ResourceManager>.Instance.Init();
        Singleton<InputManager>.Instance.Init();


        //other managers(unique in scene) should be initialized later
        Singleton<UnitManager>.Instance.Init();
        Singleton<ItemManager>.Instance.Init();
        Singleton<UIManager>.Instance.Init();
    }

    #endregion


}
