using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerAutoCombat : MonoBehaviour
{
    public float attackInterval = 2f; //자동 기본 공격 주기
    public LayerMask enemyLayer;
    private NavMeshAgent agent;
    private Transform targetEnemy;

    public UIManager uimanager;


    private float attackTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        uimanager.AutoButton(false);
    }

    private void Update()
    {
         if (!PlayerManager.Instance.Player.controller.isAutoMode) return;

        attackTimer += Time.deltaTime;

        FindEnemy();

        if (targetEnemy != null)
        {


            float distance = Vector3.Distance(transform.position, targetEnemy.position);

            if (distance > PlayerManager.Instance.Player.AttackRange)
            {
                agent.SetDestination(targetEnemy.position);
                PlayerManager.Instance.Player.controller.animationHandler?.SetMoveState(0.5f);
            }
            else
            {
                PlayerManager.Instance.Player.controller.animationHandler?.SetMoveState(0f);
                agent.ResetPath();      // 도착했으면 멈춘다.

                if (attackTimer >= attackInterval)
                {
                    attackTimer = 0;
                    Attack();
                }
            }
        }
        PlayerManager.Instance.Player.controller.skillManager.AutoUesSkill();

    }

    private void FindEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 10f, enemyLayer);
        float minDistance = float.MaxValue;
        Transform nearest = null;

        foreach (Collider hit in hits)
        {
            if (!hit.gameObject.activeSelf) continue;

            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null || enemy.isDead) continue;

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = hit.transform;
            }
        }
        targetEnemy = nearest;

    }

    private void Attack()
    {
        if (targetEnemy == null) return;
        PlayerManager.Instance.Player.controller.animationHandler?.PlayAttack();
        transform.LookAt(targetEnemy);
    }

    public void SetAutoMode(bool enable)
    {
        PlayerManager.Instance.Player.controller.isAutoMode = enable;
        PlayerManager.Instance.Player.controller.playerInputEnabled(!enable);


        if (!enable && agent.isOnNavMesh)
        {

            agent.ResetPath();
        }
        uimanager.AutoButton(enable);

        if (enable)
        {
            agent.enabled = true;
        }
        else
        {
            agent.enabled = false;
        }

        
    }

}
