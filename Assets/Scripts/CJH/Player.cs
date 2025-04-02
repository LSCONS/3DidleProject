using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string PlayerName { get; private set; }
    public int Level { get; private set; } = 1;
    public int MaxExp { get; private set; }
    public int CurrentExp { get; private set; }
    public float MaxHp { get; private set; }
    public float CurrentHP { get; private set; }
    public float Damage { get; private set; }
    public float AttackRange { get; private set; }
    public float Defence { get; private set; }

    public float CriticalChance { get; private set; }
    public float CriticalDamage { get; private set; }

    public int Gold { get; private set; } = 1000;

    public bool isWeaponEquip { get; private set; } = false;
    public int weaponIndex { get; private set; } = -1;
    public bool isHelmetEquip { get; private set; } = false;
    public int helmetIndex { get; private set; } = -1;

    private int expUp = 1;

    public PlayerController controller;
    

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        Init("기사", 500, 20, 15);
    }

    public void Init(string playerName, float maxHp, float damage, float defence)
    {
        PlayerName = playerName;
        MaxHp = maxHp;
        CurrentHP = maxHp;
        Damage = damage;
        Defence = defence;
        CriticalChance = 10;
        CriticalDamage = 150;
        AttackRange = 2;
        Gold = 1000;
        CurrentExp = 0;
        MaxExp = 50;
    }

    public void TakeDamage(float damage)
    {
        SubstractHelath(damage);
        // 피해 받는 이벤트 추가하기
    }

    public void AddHealth(float value)
    {
        CurrentHP = Mathf.Min(CurrentHP + value, MaxHp);
    }

    public void SubstractHelath(float value)
    {
        CurrentHP = Mathf.Max(CurrentHP - value, 0);
    }

    public void LevelUp()
    {
        Level++;
        CurrentExp -= MaxExp;
        if (Level % 10 == 0)
        {
            expUp++;
        }
        MaxExp = expUp * Level * 50;
    }

}
