using System;
using System.Collections.Generic;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Dead,
}

public class PlayerStateMachine : BaseStateMachine
{
    public PlayerUnit player;
    public PlayerStateMachine(PlayerUnit player)
    {
        this.player = player;

        //initialize state dictionary
        Init();
    }

    #region States
    public Dictionary<PlayerState, IState> states = new Dictionary<PlayerState, IState>();

    protected override void Init()
    {
        states.Add(PlayerState.Idle, new PlayerIdleState(this));
        states.Add(PlayerState.Walk, new PlayerWalkState(this));
        states.Add(PlayerState.Run, new PlayerRunState(this));
        states.Add(PlayerState.Dead, new PlayerDeadState(this));
    }
    #endregion

    #region Parameters
    public bool shouldRun;

    #endregion

    
}
