using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour, IMonsterBehaivor<EnemyController>
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
        enemyController.agent.isStopped = false;
        NavMeshPath path = new NavMeshPath();
        if (enemyController.agent.CalculatePath(enemyController.character.transform.position,path))//enemyController.agent.CalculatePath(GameManager.Instance.Player.transform.position, path)
        {
            enemyController.agent.SetDestination(enemyController.character.transform.position);
            //enemyController.agent.SetDestination(GameManager.Instance.Player.transform.position);
        }
    }
}
