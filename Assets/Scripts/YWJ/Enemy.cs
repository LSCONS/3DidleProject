using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float power;
    [SerializeField] private float defence;

    public float Power 
    {
        get {  return power; } private set { power = value; } 
    }

    private MonsterManager monsterManager;


    private void Start()
    {
        monsterManager = MonsterManager.Instance;
    }
    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;
        
        health -= (damage-defence);
        if (health <= 0)
        {
            Destroy(this.gameObject);
            monsterManager.curMonsters.Remove(this.gameObject);
        }
    }
}
