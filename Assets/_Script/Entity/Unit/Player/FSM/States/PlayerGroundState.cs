using System;
using UnityEngine;

public class PlayerGroundState : IState
{
    //ref
    protected PlayerUnitSO playerData => playerStateMachine.player.data;
    protected Rigidbody rigidbody => playerStateMachine.player.rb;
    protected InputManager input => Singleton<InputManager>.Instance;



    protected PlayerStateMachine playerStateMachine;
    public PlayerGroundState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void HandleInput()
    {
        playerStateMachine.shouldRun = input.locomotionToggleInput;


    }

    public virtual void OnUpdate()
    {
        
    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }

    #region Transitions

    #endregion

    #region ReusableMethods

    #endregion


}
