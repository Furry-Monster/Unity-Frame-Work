//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这里管理游戏的状态机，
//          包括游戏开始，初始化，加载，游戏中，暂停,重置等状态。
//          
//          对于游戏状态的划分，我没有很多经验，
//          所以这里的划分可能比较粗糙，需要根据实际情况进行调整。
//==========================

public class GameManager : Singleton<GameManager>
{

    //fsm
    private GameStateMachine gameFSM;

    protected override void Awake()
    {
        base.Awake();

        gameFSM = new GameStateMachine(this);
    }

    //kick of the game state machine
    private void Start() => gameFSM.ChangeState(gameFSM.states[GameState.Load]);

    private void Update()
    {
        //firstly check if there is any transision in FSM;
        gameFSM.HandleInput();
        //then update the FSM
        gameFSM.UpdateStateMachine();
    }

    private void FixedUpdate()
    {
        gameFSM.FixedUpdateStateMachine();
    }

    
}
