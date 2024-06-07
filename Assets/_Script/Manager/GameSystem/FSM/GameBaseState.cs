public class GameBaseState : IState
{
    //ref

    protected GameStateMachine gameStateMachine;
    public GameBaseState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void HandleInput()
    {

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
