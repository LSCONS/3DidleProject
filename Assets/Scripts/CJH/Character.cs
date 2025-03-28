using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string PlayerName { get; private set; }
    public int Level { get; private set; } = 1;
    public int MaxExp { get; private set; }
    public int CurrentExp { get; private set; }
    public float MaxHp { get; private set; }
    public float CurrentHP { get; private set; }
    public float Damage { get; private set; }
    public float Defence { get; private set; }

    public float CriticalChance { get; private set; }
    public float CriticalDamage { get; private set; }

    public int Gold { get; private set; } = 1000;

    public bool isWeaponEquip { get; private set; } = false;
    public int weaponIndex { get; private set; } = -1;
    public bool isHelmetEquip { get; private set; } = false;
    public int helmetIndex { get; private set; } = -1;

    public Character(string playerName, float maxHp, float damage, float defence)
    {
        PlayerName = playerName;
        MaxHp = maxHp;
        CurrentHP = maxHp;
        Damage = damage;
        Defence = defence;
        CriticalChance = 10;
        CriticalDamage = 150;
    }

}
