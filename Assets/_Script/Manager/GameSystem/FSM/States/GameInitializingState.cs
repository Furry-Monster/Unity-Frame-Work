/*------------------------------
// Task description:
//  - These managers need to be initialized every time the scene changed:
//    - UnitManager
//    - ItemManager
//    - ObjectPoolManager
//    - UIManager
//    - AudioManager
//    
//  - Change state to LoadState.
--------------------------------*/

public class GameInitializingState : GameBaseState
{

    public GameInitializingState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();

        InitAll();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (gameStateMachine.isInit)
        {
            gameStateMachine.ChangeState(gameStateMachine.states[GameState.Load]);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

    }

    public override void OnExit()
    {
        base.OnExit();

    }

    #region Transition

    #endregion

    #region Reusable
    private void InitAll()
    {
        //critically important managers(global) should be initialized when awake
        // -GameSceneManager
        // -Loadings
        //   --AssetBundleManager
        //   --ResourceManager
        // -InputManager
        Singleton<GameSceneManager>.Instance.Init();    
        Singleton<AssetBundleManager>.Instance.Init();
        Singleton<ResourceManager>.Instance.Init();
        Singleton<InputManager>.Instance.Init();

        //other managers(unique in scene) should be initialized later
        // -Entities
        //   --UnitManager(Manage all the units in game)
        //   --ItemManager(Manage all the item in game)
        //   --ObjectPoolManager(Manage reusable eneity objects)
        // -Media
        //   --UIManager(Manage all the ui in game)
        //   --AudioManager(Manage all the music and sounds in game)
        Singleton<UnitManager>.Instance.Init();
        Singleton<ItemManager>.Instance.Init();
        Singleton<ObjectPoolManager>.Instance.Init();
        Singleton<UIManager>.Instance.Init();
        Singleton<AudioManager>.Instance.Init();

        gameStateMachine.isInit = true;
    }

    #endregion
}
