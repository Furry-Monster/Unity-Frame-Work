using System;

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

        OnAfterStateChanged?.Invoke(newState);
    }

    //Handlers for state entering
    private void HandleStartingEnter()
    {
        //not clear about what should be done here.....

        ChangeState(Singleton<Systems>.Instance.isInit ? GameState.Initializing : GameState.Loading);
    }
    private void HandleInitializingEnter()
    {
        //system init,init data,set references,etc.


        Singleton<Systems>.Instance.isInit = true;
        ChangeState(GameState.Menu);
    }
    private void HandleMenuEnter()
    {

    }
    private void HandleLoadingEnter()
    {
        //load game data, create player, etc.
        Singleton<UnitManager>.Instance.SpawnUnit(Singleton<UnitManager>.Instance.unitDict[UnitType.Player]);
        Singleton<InputManager>.Instance.ChangeInputType(InputType.Player);

        ChangeState(GameState.GamePlay);
    }
    private void HandleGamePlayEnter()
    {

    }
    private void HandlePausedEnter()
    {

    }
}
