using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour, IMonsterBehaivor<EnemyController>
{
    private EnemyController enemyController;
    public void EnterBehaivor(EnemyController monsterController)
    {
        enemyController = monsterController;
    }

    public void ExitBehaivor(EnemyController monsterController)
    {
        
    }

    public void UpdateBehavior(EnemyController monsterController)
    {
        enemyController.agent.isStopped = true;
        if (Time.time - enemyController.lastAttackTime > enemyController.attackRate)
        {
            enemyController.lastAttackTime = Time.time;
            // 플레이어가 공격받는 로직
            enemyController.animator.SetTrigger("Attack");
        }
    }
}
