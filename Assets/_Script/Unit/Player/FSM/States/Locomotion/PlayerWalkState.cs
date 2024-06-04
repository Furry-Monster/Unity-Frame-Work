using UnityEngine;
public class PlayerWalkState : PlayerGroundState
{

    //cache
    private Vector2 _moveInput;
    private float _targetSpeed;
    private float _currentSpeed;
    private Vector3 _targetRotation;
    private Vector3 _currentRotation;

    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        _currentSpeed = rigidbody.velocity.magnitude;
        _currentRotation = playerStateMachine.player.transform.forward;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        _moveInput = input.moveInput;

        ToIdle();
        ToRun();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Rotate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        Move();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    #region Transitions
    private void ToIdle()
    {
        if (rigidbody.velocity.magnitude < 0.1 && _moveInput.magnitude < 0.1)
        {
            playerStateMachine.ChangeState(playerStateMachine.states[PlayerState.Idle]);
        }
    }

    private void ToRun()
    {
        if (playerStateMachine.shouldRun)
        {
            playerStateMachine.ChangeState(playerStateMachine.states[PlayerState.Run]);
        }
    }
    #endregion

    #region ReusableMethods

    private void Move()
    {
        Vector3 _moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);

        if (_moveInput.magnitude > 0.1f)
        {
            _targetSpeed = playerData.walkSpeed;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * playerData.acceleration);
        }
        else
        {
            _targetSpeed = 0f;
            _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * playerData.decceleration);
        }

        rigidbody.velocity = _currentSpeed * _moveDir;
    }

    private void Rotate()
    {
        Vector3 _moveDir = new Vector3(_moveInput.x, 0, _moveInput.y);

        if (_moveDir != Vector3.zero)
        {
            _targetRotation = _moveDir;
            _currentRotation = Vector3.Lerp(_currentRotation, _targetRotation, Time.deltaTime * playerData.turnSpeed);
        }

        playerStateMachine.player.transform.forward = _currentRotation;
    }
    #endregion
}
