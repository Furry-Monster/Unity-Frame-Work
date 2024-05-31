using UnityEngine;
public class PlayerRunState : PlayerBaseState
{
    //cache
    private Vector2 _moveInput;
    private float _targetSpeed;
    private float _currentSpeed;

    public PlayerRunState(PlayerStateMachine playerStateMachine):base(playerStateMachine)
    {
        
    }

    public override void OnEnter()
    {
        _currentSpeed = rigidbody.velocity.magnitude;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        _moveInput = input.moveInput;

        ToIdle();
        ToWalk();
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        Move();
    }

    public override void OnExit()
    {

    }

    #region Transitions
    private void ToIdle()
    {
        if (rigidbody.velocity.magnitude < 0.1 && _moveInput.magnitude < 0.1)
        {
            playerStateMachine.ChangeState(playerStateMachine.states[PlayerState.Idle]);
        }
    }

    private void ToWalk()
    {
        if (!playerStateMachine.shouldRun)
        {
            playerStateMachine.ChangeState(playerStateMachine.states[PlayerState.Walk]);
        }
    }
    #endregion

    #region ReusableMethods
    private void Move()
    {
        Vector3 _moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);
        if (_moveInput.magnitude > 0.1f)
        {
            _targetSpeed = playerData.runSpeed;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * playerData.acceleration);
        }
        else
        {
            _targetSpeed = 0f;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * playerData.decceleration);
        }

        rigidbody.velocity = _currentSpeed * _moveDir;
    }
    #endregion
}
