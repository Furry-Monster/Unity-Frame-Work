public class PlayerDeadState : PlayerBaseState
{ 
    public PlayerDeadState(PlayerStateMachine playerStateMachine):base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {

    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }

    #region Transitions

    #endregion

    #region ReusableMethods

    #endregion
}
