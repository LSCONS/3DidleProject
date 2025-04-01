using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum MonsterBehavior
    {
        Move,
        Attack,
    }

    private IMonsterBehaivor<EnemyController> monsterBehaivor;

    public float enemySpeed = 3f;
    public GameObject character;

    private Dictionary<MonsterBehavior, IMonsterBehaivor<EnemyController>> curState = new Dictionary<MonsterBehavior, IMonsterBehaivor<EnemyController>>();
    private MonsterBehaivorMachine<EnemyController> stateMachine;
    public NavMeshAgent agent;
    public float lastAttackTime;
    public float attackRate = 0.5f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
        IMonsterBehaivor<EnemyController> move = new MonsterMove();
        IMonsterBehaivor<EnemyController> attack = new MonsterAttack();
        agent.speed = enemySpeed;

        curState.Add(MonsterBehavior.Move, move);
        curState.Add(MonsterBehavior.Attack, attack);

        stateMachine = new MonsterBehaivorMachine<EnemyController>(this, curState[MonsterBehavior.Move]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position, PlayerManager.Instance.PlayerTransform.position) <= 1.5f)
        {
            stateMachine.SetState(curState[MonsterBehavior.Attack]);
        }
        if(Vector3.Distance(this.gameObject.transform.position, PlayerManager.Instance.PlayerTransform.position) > 1.5f)
        {
            stateMachine.SetState(curState[MonsterBehavior.Move]);
        }
        stateMachine.UpdateState();
    }
}
