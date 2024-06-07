//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          ���������Ϸ��״̬����
//          ������Ϸ��ʼ����ʼ�������أ���Ϸ�У���ͣ,���õ�״̬��
//          
//          ������Ϸ״̬�Ļ��֣���û�кܶྭ�飬
//          ��������Ļ��ֿ��ܱȽϴֲڣ���Ҫ����ʵ��������е�����
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
    private void Start() => gameFSM.ChangeState(gameFSM.states[GameState.Initializing]);

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
