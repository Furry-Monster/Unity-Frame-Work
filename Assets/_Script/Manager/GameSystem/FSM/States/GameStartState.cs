/*------------------------------
// Task description:
//  - The game start state is the first state of the gamePlay part.
//  - I'd like to use a vignette effect to make the game start more interesting.
//  - The vignette effect should last for 2 seconds.
//  - After 2 seconds, the game state should change to the play state.
--------------------------------*/
public class GameStartState : GameBaseState
{
    public GameStartState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        //just for debug
        Singleton<TimerSystem>.Instance.Schedule((interval) =>
        {
            gameStateMachine.ChangeState(gameStateMachine.states[GameState.Play]);

            Singleton<InputManager>.Instance.ChangeInputType(InputType.Player);
        }, 2f);
    }

    public override void HandleInput()
    {
        base.HandleInput();


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

    #region Transition

    #endregion

    #region Reusable


    #endregion
}
