using UnityEngine;

public abstract class BaseStateMachine
{
    protected IState currentState;
    //if needed , a "lastState" could be set here

    //interface
    protected abstract void Init();

    //main methods
    public void ChangeState(IState newState)
    {
        currentState?.OnExit();

        currentState = newState;
        newState?.OnEnter();

        Debug.Log($"{GetType().Name} set state to {newState.GetType().Name}");
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void UpdateStateMachine()
    {
        currentState?.OnUpdate();
    }

    public void FixedUpdateStateMachine()
    {
        currentState?.OnFixedUpdate();
    }
}
