using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_AutoAttack : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform targetEnemy;
    private bool isAutoAttacking = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
         if (!isAutoAttacking || targetEnemy == null) return;

        float distance = Vector3.Distance(transform.position, targetEnemy.position);

        if (distance > PlayerManager.Instance.Player.AttackRange)
        {
            agent.SetDestination(targetEnemy.position);
        }
        else
        {
            agent.ResetPath();
            transform.LookAt(targetEnemy);
            // 공격 부분 추가
        }


    }

}
