using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool isBoss;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        IMonsterBehaivor<EnemyController> move = GetComponent<MonsterMove>();
        IMonsterBehaivor<EnemyController> attack = GetComponent <MonsterAttack>();
        agent.speed = enemySpeed;

        curState.Add(MonsterBehavior.Move, move);
        curState.Add(MonsterBehavior.Attack, attack);

        stateMachine = new MonsterBehaivorMachine<EnemyController>(this, curState[MonsterBehavior.Move]);
    }

    // Update is called once per frame
    void Update()
    {
        Collider playerCollider = PlayerManager.Instance.PlayerTransform.GetComponent<Collider>();
        Collider myCollider = GetComponent<Collider>();
        if (playerCollider == null || myCollider == null) return;
        Vector3 closetPoint = playerCollider.ClosestPoint(transform.position);
        Vector3 myColset = myCollider.ClosestPoint(PlayerManager.Instance.PlayerTransform.position);
        if (!isBoss)
        {
            if (Vector3.Distance(myColset, closetPoint) <= 2f)
            {
                stateMachine.SetState(curState[MonsterBehavior.Attack]);
            }
            if (Vector3.Distance(myColset, closetPoint) > 2f)
            {
                stateMachine.SetState(curState[MonsterBehavior.Move]);
            }
        }
        else
        {
            if (Vector3.Distance(myColset, closetPoint) <= 6f)
            {
                stateMachine.SetState(curState[MonsterBehavior.Attack]);
            }
            if (Vector3.Distance(myColset, closetPoint) > 6f)
            {
                stateMachine.SetState(curState[MonsterBehavior.Move]);
            }
        }
        stateMachine.UpdateState();
    }
}
