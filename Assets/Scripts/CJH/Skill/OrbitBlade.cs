using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OrbitBlade : MonoBehaviour
{
    private Transform target;
    private float orbitRadius;
    private float orbitSpeed;
    private float selfSpinSpeed;
    private float angle;
    private float yOffset = 0.5f;

    public void Init(Transform target, float radius, float orbitSpeed, float selfSpineSpeed, float StartAngel)
    {
        this.target = target;
        this.orbitRadius = radius;
        this.orbitSpeed = orbitSpeed;
        this.selfSpinSpeed = selfSpineSpeed;
        this.angle = StartAngel;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogError("OrbitBalde.cs : target이 없습니다.");
            return;
        }

        angle += orbitSpeed * Time.deltaTime;
        Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward * orbitRadius;
        Vector3 targetPos = target.position + offset;
        targetPos.y += yOffset; // 캐릭터의 허리부분으로 오도록 yOffset 값을 조정
        transform.position = targetPos;

        // 자전
        transform.Rotate(Vector3.up * selfSpinSpeed * Time.deltaTime);

    }

    public void TriggerHit(float damage)
    {
        float range = 1f;
        Collider[] enemies = Physics.OverlapSphere(transform.position, range);
        foreach (var enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, 1f);
    }




}
