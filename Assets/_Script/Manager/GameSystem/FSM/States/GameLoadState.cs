/*------------------------------
// Task description:
//  - Load all the game resources and prepare the game environment.
//  - Switch to the "Start" state.
--------------------------------*/
using UnityEngine;

public class GameLoadState : GameBaseState
{
    public GameLoadState(GameStateMachine gameStateMachine) : base(gameStateMachine)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        LoadResources();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (gameStateMachine.isLoad)
        {
            gameStateMachine.ChangeState(gameStateMachine.states[GameState.Start]);
        }
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
    private void LoadResources()
    {
        GameObject playerPrefab = Singleton<AssetBundleManager>.Instance.LoadAsset<GameObject>("character", "Player");
        GameObject stickPrefab = Singleton<AssetBundleManager>.Instance.LoadAsset<GameObject>("item", "Stick");

        Singleton<UnitManager>.Instance.AddUnit(playerPrefab.GetComponent<PlayerUnit>().unit);
        Singleton<ItemManager>.Instance.AddItem(stickPrefab.GetComponent<ItemInstanceContainer>().itemInstance);

        Singleton<UnitManager>.Instance.SpawnUnit(Singleton<UnitManager>.Instance.unitDict["Player".GetHashCode()]);
        Singleton<ItemManager>.Instance.SpawnItem("Stick".GetHashCode());

        gameStateMachine.isLoad = true;
    }

    #endregion
}
