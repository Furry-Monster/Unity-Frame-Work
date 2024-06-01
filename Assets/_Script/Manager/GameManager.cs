using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnBeforeStateChanged;
    public static event Action OnAfterStateChanged;

    public GameState currentState { get; private set; }

    //kick of the game state machine
    private void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState)
    {
        OnBeforeStateChanged?.Invoke();

        currentState = newState;

        switch (currentState)
        {
            case GameState.Starting:
                HandleStartingEnter();
                break;
            case GameState.Initializing:
                HandleInitializingEnter();
                break;
            case GameState.Running:
                HandleRunningEnter();
                break;
            case GameState.Paused:
                HandlePausedEnter();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke();
    }

    //Handlers for state entering
    private void HandleStartingEnter()
    {
        ChangeState(GameState.Initializing);
    }
    private void HandleInitializingEnter()
    {
        //load game data, create player, etc.
        Singleton<UnitManager>.Instance.SpawnUnit(Singleton<UnitManager>.Instance.unitDict[UnitType.Player]);
        
        ChangeState(GameState.Running);
    }
    private void HandleRunningEnter()
    {
        
    }
    private void HandlePausedEnter()
    {

    }

}

/// <summary>
/// States that your game might be in
/// </summary>
[Serializable]
public enum GameState
{
    Starting,
    Initializing,
    Running,
    Paused,
}
