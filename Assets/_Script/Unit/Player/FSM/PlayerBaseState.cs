using UnityEngine;

public class PlayerBaseState : IState
{
    //ref
    protected PlayerUnitSO playerData => playerStateMachine.player.data;
    protected Rigidbody rigidbody => playerStateMachine.player.rb;
    protected InputManager input => Singleton<InputManager>.Instance;


    protected PlayerStateMachine playerStateMachine;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
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
}
