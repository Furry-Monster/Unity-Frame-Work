using UnityEngine;
public class PlayerIdleState : PlayerGroundState
{
    //cache
    private Vector2 _moveInput;

    public PlayerIdleState(PlayerStateMachine playerStateMachine):base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        ResetVelocity();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        _moveInput = input.moveInput;

        //can change to walk or run state
        ToLocomotion();

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

    #region Transitions
    private void ToLocomotion()
    {
        if (_moveInput.magnitude > 0.1f)
        {
            if (playerStateMachine.shouldRun)
            {
                playerStateMachine.ChangeState(playerStateMachine.states[PlayerState.Run]);
            }
            else
            {
                playerStateMachine.ChangeState(playerStateMachine.states[PlayerState.Walk]);
            }
        }
    }

    #endregion

    #region ReusableMethods
    private void ResetVelocity()
    {
        if (rigidbody.velocity.magnitude > 0.1f)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }

    #endregion
}
