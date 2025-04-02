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


    //업그레이드 성공 시 실행할 메서드
    private void UpgradeItem()
    {
        if (Data.Type == ItemType.EquipItem ||
            Data.equipItemData != null)
        {
            EquipItemData data = Data.equipItemData[0];

            if(data.UpgradePrice > PlayerManager.Instance.player.Gold)
            {
                //TODO: 골드 부족으로 강화 불가능 메시지 필요
                return;
            }
            //TODO: 플레이어의 골드를 빼는 명령어 필요
            Data.GradeCount++;
            AddEquipItemValue(data, data.UpgradeAttackValue, data.DefenceAttackValue);
        }
    }


    //장비 데이터에 공격력, 방어력을 올리는 메서드
    private void AddEquipItemValue(EquipItemData data, int attackValue, int defenceValue)
    {
        data.AttackValue += attackValue;
        data.DefenceValue += defenceValue;
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


    /// <summary>
    /// 아이템을 사용할 메서드
    /// </summary>
    public void UseUsableItem()
    {
        for(int i = 0; i < Data.useItemDatas.Length; i++)
        {
            UseItemData useItemData = Data.useItemDatas[i];
            switch (useItemData.UseType)
            {
                case UseItemType.HP:
                    PlayerManager.Instance.player.AddHealth(useItemData.HealthValue);
                    break;

                case UseItemType.MP:
                    PlayerManager.Instance.player.AddMana(useItemData.HealthValue);
                    break;
            }
        }
    }
}
