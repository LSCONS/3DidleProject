using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    UseItem,        //사용 아이템
    EquipItem,      //장비 아이템
    Resource,       //자원 아이템
}

public enum EquipItemType
{
    Helmet,       //헬멧 
    Armor,        //갑바
    Shoes,        //신발
    Necklace,     //목걸이
    Ring,         //반지
    Weapon,       //무기
}

public enum UseItemType
{
    HP,     //체력
    MP      //마나
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public int ID;
    public Sprite Icon;
    public string Name;
    public string Description;
    public int Price;
    public ItemType Type;
    public UseItemData[] useItemDatas;
    public EquipItemData[] equipItemData;

    public bool IsStack;
    public int MaxStack;
}


[Serializable]
public class UseItemData
{
    public UseItemType UseType;
    public int HealthValue;
}


[Serializable]
public class EquipItemData
{
    public EquipItemType EquipType;
    public int AttackValue;
    public int ArmorValue;
}
