using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public string PlayerName { get; private set; }
    public int Level { get; private set; } = 1;
    public int MaxExp { get; private set; }
    public int CurrentExp { get; private set; }
    public float MaxHp { get; private set; }
    public float CurrentHP { get; private set; }
    public float MaxMp { get; private set; }
    public float CurrentMp { get; private set; }
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
    private bool isDead = false;

    public PlayerController controller;
    

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        Init("기사", 1000f, 20f, 15f);
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
        if (CurrentHP <= 0)
        {
            PlayerDeath();
        }
    }

    public void AddHealth(float value)
    {
        CurrentHP = Mathf.Min(CurrentHP + value, MaxHp);
    }

    public void SubstractHelath(float value)
    {
        if (CurrentHP - value >= 0)
        {
            Debug.Log("HP가 없습니다.");
        }
        CurrentHP = Mathf.Max(CurrentHP - value, 0);
    }

    public void AddMana(float value)
    {
        CurrentMp = Mathf.Min(CurrentMp + value, MaxMp);
    }

    public void Substract(float value)
    {
        if (CurrentMp - value >= 0)
        {
            Debug.Log("MP가 없습니다.");
        }
        CurrentMp = Mathf.Min(CurrentMp - value, 0);
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

        Damage++;
        Defence += 0.5f;
        MaxHp += 5f;
        MaxMp += 1f;
        AddHealth(MaxHp);
        AddMana(MaxMp);
    }

    public void AddExp(int value)
    {
        CurrentExp += value;
        if (CurrentExp > MaxExp)
        {
            LevelUp();
        }
    }


    public void  SetGold(int amount)
    {
        Gold = Mathf.Max(0, amount);
    }

    public bool TrySpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            return true;
        }
        return false;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
    }
    public void PlayerDeath()
    {
        if (CurrentHP <= 0 && !isDead)
        {
            isDead = true;

            controller.playerInputEnabled(false);
            controller.enabled = false;
            controller.rb.velocity = Vector3.zero;
            controller.isAutoMode = false;

            GetComponent<PlayerAutoCombat>().SetAutoMode(false);
            GetComponent<PlayerAutoCombat>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            controller.animationHandler?.PlayDead();

        }

    }
    //장착적용
    public void AddEquipStats(int atk, int def)
    {
        Damage += atk;
        Defence += def;
    }
    //장착해제
    public void RemoveEquipStats(int atk, int def)
    {
        Damage -= atk;
        Defence -= def;
    }

}
