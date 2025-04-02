using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float power;
    [SerializeField] private float defence;
    public bool isDead = false;
    private float maxHealth;

    public float Power 
    {
        get {  return power; } private set { power = value; } 
    }

    private MonsterManager monsterManager;


    private void Start()
    {
        monsterManager = MonsterManager.Instance;
        maxHealth = health;
    }

    public void Init()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;
        
        health -= (damage-defence);
        if (health <= 0)
        {
            isDead = true;
            this.gameObject.SetActive(false);
            PlayerManager.Instance.player.AddExp(Random.Range(10,21));
        }
    }
}
