using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public static int GradeMax = 20;            //최대 업그레이드 수
    public static float ProbabilityMin = 10f;   //최소 확률
    public ItemData Data;
    public Item(ItemData _itemData)
    {
        Data = _itemData;
    }


    /// <summary>
    /// 업그레이드를 시도할 메서드
    /// </summary>
    public void TryUpgradeItem()
    {
        int GradeCount = Data.GradeCount;
        if (GradeCount < GradeMax)
        {
            float Probability = 100 - ((100 - ProbabilityMin) / GradeMax) * GradeCount;
            if (Util.ComputeProbability(Probability))
            {
                UpgradeItem();
                //TODO: 강화 성공. 강화 성공 메시지 필요.
            }
            else
            {
                //TODO: 강화 실패. 강화 실패 메시지 필요.
            }
        }
        else
        {
            //TODO: 강화 최대 횟수 도달. 강화 불가 메시지 필요.
        }
    }


    //업그레이드 성공 시 실행할 메서드
    private void UpgradeItem()
    {
        if (Data.Type == ItemType.EquipItem ||
            Data.equipItemData != null)
        {
            EquipItemData data = Data.equipItemData[0];

            if(data.UpgradePrice > 100)//TODO: 100 대신 플레이어 소지 머니로 바꿔야 함.
            {
                //TODO: 골드 부족으로 강화 불가능 메시지 필요
                return;
            }
            //TODO: 플레이어의 골드를 빼는 명령어 필요
            Data.GradeCount++;
            AddEquipItemValue(data, data.UpgradeAttackValue, data.DefenceAttackValue);
        }
    }


    public void AddEquipItemValue(EquipItemData data, int attackValue, int defenceValue)
    {
        data.AttackValue += attackValue;
        data.DefenceValue += defenceValue;
    }
}
