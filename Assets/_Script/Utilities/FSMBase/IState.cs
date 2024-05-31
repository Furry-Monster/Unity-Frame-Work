public interface IState
{
    /// <summary>
    /// Called when Enter a new state
    /// </summary>
    public void OnEnter();

    /// <summary>
    /// Called to check if the state can transition to another state
    /// </summary>
    public void HandleInput();

    /// <summary>
    /// Called every frame
    /// </summary>
    public void OnUpdate();

    /// <summary>
    /// Called every fixed frame
    /// </summary>
    public void OnFixedUpdate();

    /// <summary>
    /// Called when Exit from a state
    /// </summary>
    public void OnExit();
}
