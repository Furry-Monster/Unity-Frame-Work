using System;
using System.Collections.Generic;


/// <summary>
/// States that your game might be in
/// </summary>
[Serializable]
public enum GameState
{
    Load,      // not including load asset,only load the game scene and set the player's position
    Start,     // player enter timeline or something like that
    Play,
    Paused,
    Reset         // reset to the start or reload the level
}

public class GameStateMachine : BaseStateMachine
{
    private GameManager gameManager;
    public GameStateMachine(GameManager gameManager)
    {
        this.gameManager = gameManager;

        //init the game States
        Init();
    }

    #region State
    public Dictionary<GameState, GameBaseState> states = new Dictionary<GameState, GameBaseState>();

    protected override void Init()
    {
        states.Add(GameState.Load, new GameLoadState(this));
        states.Add(GameState.Start, new GameStartState(this));
        states.Add(GameState.Play, new GamePlayState(this));
        states.Add(GameState.Paused, new GamePauseState(this));
        states.Add(GameState.Reset, new GameResetState(this));
    }

    #endregion

    #region parameters
    public bool isLoad = false;
    #endregion
}
