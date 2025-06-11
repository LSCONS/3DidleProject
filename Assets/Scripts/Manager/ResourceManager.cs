using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    #region 종류별 아이템 데이터 정리
    public ItemData[] ArmorItemDatas;       //모든 갑옷 아이템
    public ItemData[] HelmetItemDatas;      //모든 헬멧 아이템
    public ItemData[] NecklaceItemDatas;    //모든 목걸이 아이템
    public ItemData[] RingItemDatas;        //모든 반지 아이템
    public ItemData[] ShoesItemDatas;       //모든 신발 아이템
    public ItemData[] WeaponItemDatas;      //모든 무기 아이템
    public ItemData[] PotionHPItemDatas;    //모든 HP포션 아이템
    public ItemData[] PotionMPItemDatas;    //모든 MP포션 아이템
    #endregion

    #region 타입별 아이템 데이터 리스트 정리
    public List<ItemData> EquipItemDataList = new();    //모든 장비 아이템
    public List<ItemData> UseItemDataList   = new();    //모든 사용 아이템
    public List<ItemData> AllItemDataList   = new();    //모든 아이템
    #endregion

    #region 등급별 아이템 데이터 리스트 정리
    public List<ItemData> CommonDataList    = new();    //모든 Common등급 아이템
    public List<ItemData> RareDataList      = new();    //모든 Rare등급 아이템
    public List<ItemData> SuperRareDataList = new();    //모든 SuperRare등급 아이템
    public List<ItemData> EpicDataList      = new();    //모든 Epic등급 아이템
    public List<ItemData> LegendDataList    = new();    //모든 Legend등급 아이템

    public List<GradeProbability> GradeProbabilities = new();   //등급별 확률을 저장할 리스트
    #endregion

    #region 등급별 확률 정리
    private int probabilityCommon = 40;
    private int probabilityRare = 30;
    private int probabilitySuperRare = 20;
    private int probabilityEpic = 7;
    private int probabilityLegend = 3;
    #endregion

    private const string equipItemPath  = "ItemData/EquipItem";
    private const string useItemPath    = "ItemData/UseItem";

    protected override void Awake()
    {
        base.Awake();
        InitDatas();        //Resource의 데이터를 모두 통합
        InitItemList();     //ItemList에 따라 다양한 아이템을 정리하고 집어넣는 메서드
    }


    //Resource의 데이터를 모두 통합
    private void InitDatas()
    {
        ArmorItemDatas      = Resources.LoadAll<ItemData>(equipItemPath + "/Armor");
        HelmetItemDatas     = Resources.LoadAll<ItemData>(equipItemPath + "/Helmet");
        NecklaceItemDatas   = Resources.LoadAll<ItemData>(equipItemPath + "/Necklace");
        RingItemDatas       = Resources.LoadAll<ItemData>(equipItemPath + "/Ring");
        ShoesItemDatas      = Resources.LoadAll<ItemData>(equipItemPath + "/Shoes");
        WeaponItemDatas     = Resources.LoadAll<ItemData>(equipItemPath + "/Weapon");
        PotionHPItemDatas   = Resources.LoadAll<ItemData>(useItemPath   + "/PotionHP");
        PotionMPItemDatas   = Resources.LoadAll<ItemData>(useItemPath   + "/PotionMP");
    }


    //ItemList에 따라 다양한 아이템을 정리하고 집어넣는 메서드
    private void InitItemList()
    {
        InitEquipList();        //장비 아이템 리스트 초기화
        InitUseList();          //사용 아이템 리스트 초기화
        InitAllList();          //모든 아이템 리스트 초기화
        InitRarityList();       //모든 등급별 아이템 리스트 초기화
        InitGradeList();        //아이템 등급별 뽑기 확률 리스트 초기화
    }


    //장비 아이템 List를 정렬하는 메서드
    private void InitEquipList()
    {
        EquipItemDataList.Clear();
        AddRangeItemList(EquipItemDataList, ArmorItemDatas);
        AddRangeItemList(EquipItemDataList, HelmetItemDatas);
        AddRangeItemList(EquipItemDataList, NecklaceItemDatas);
        AddRangeItemList(EquipItemDataList, RingItemDatas);
        AddRangeItemList(EquipItemDataList, ShoesItemDatas);
        AddRangeItemList(EquipItemDataList, WeaponItemDatas);
    }


    //소비 아이템 List를 정렬하는 메서드
    private void InitUseList()
    {
        UseItemDataList.Clear();
        AddRangeItemList(UseItemDataList, PotionHPItemDatas);
        AddRangeItemList(UseItemDataList, PotionMPItemDatas);
    }


    //모든 아이템 List를 정렬하는 메서드
    private void InitAllList()
    {
        AllItemDataList.Clear();
        AllItemDataList.AddRange(EquipItemDataList);
        AllItemDataList.AddRange(UseItemDataList);
    }


    //레어 등급에 따라 아이템을 분류에서 집어넣는 메서드
    private void InitRarityList()
    {
        for (int i = 0; i < AllItemDataList.Count; i++)
        {
            switch (AllItemDataList[i].Rarity)
            {
                case ItemRarity.Common:
                    CommonDataList.Add(AllItemDataList[i]);
                    break;

                case ItemRarity.Rare:
                    RareDataList.Add(AllItemDataList[i]);
                    break;

                case ItemRarity.SuperRare:
                    SuperRareDataList.Add(AllItemDataList[i]);
                    break;

                case ItemRarity.Epic:
                    EpicDataList.Add(AllItemDataList[i]);
                    break;

                case ItemRarity.Legend:
                    LegendDataList.Add(AllItemDataList[i]);
                    break;
            }
        }
    }


    //등급별 확률을 저장할 리스트를 초기화할 메서드
    private void InitGradeList()
    {
        GradeProbabilities = new List<GradeProbability>
        {
            new GradeProbability{Rarity = ItemRarity.Common,    Weight = probabilityCommon},
            new GradeProbability{Rarity = ItemRarity.Rare,      Weight = probabilityRare},
            new GradeProbability{Rarity = ItemRarity.SuperRare, Weight = probabilitySuperRare},
            new GradeProbability{Rarity = ItemRarity.Epic,      Weight = probabilityEpic},
            new GradeProbability{Rarity = ItemRarity.Legend,    Weight = probabilityLegend}
        };
    }


    //AllItemList를 추가하는 메서드
    private void AddRangeItemList(List<ItemData> list,ItemData[] datas)
    {
        if(datas != null) list.AddRange(datas);
    }


    // 등급 별 확률에 따라 특정 등급을 반환해주는 메서드
    private ItemRarity GetRandomItemRarity()
    {
        int totalWeight = 0;
        foreach (GradeProbability value in GradeProbabilities)
        {
            totalWeight += value.Weight;
        }

        int rand = Random.Range(0, totalWeight);

        int temp = 0;
        foreach (GradeProbability value in GradeProbabilities)
        {
            temp += value.Weight;
            if (rand < temp)
            {
                return value.Rarity;
            }
        }

        Debug.LogError("Rarity is null");
        return ItemRarity.Common;
    }


    // 랜덤으로 ItemData을 반환하는 메서드
    private ItemData GetRandomItemData(List<ItemData> list)
    {
        int rand = Random.Range(0, list.Count);
        return Instantiate(list[rand]);
    }

    
    /// <summary>
    /// 랜덤으로 모든 아이템 중 한Item을 반환하는 메서드
    /// </summary>
    /// <returns>무작위 Item을 반환</returns>
    public Item GetRandomItem()
    {
        return new Item(GetRandomItemData(AllItemDataList));
    }


    /// <summary>
    /// 랜덤으로 모든 장비 아이템 중 한 Item을 반환하는 메서드
    /// </summary>
    /// <returns>무작위 장비 Item을 반환</returns>
    public Item GetRandomEquipItem()
    {
        return new Item(GetRandomItemData(EquipItemDataList));
    }


    /// <summary>
    /// 랜덤으로 모든 사용 아이템 중 한 Item을 반환하는 메서드
    /// </summary>
    /// <returns>무작위 사용 Item을 반환</returns>
    public Item GetRandomUseItem()
    {
        return new Item(GetRandomItemData(UseItemDataList));
    }


    /// <summary>
    /// 랜덤으로 리스트에 있는 한 Item을 반환하는 메서드
    /// ResourceManager에 있는 List 중 가져올 List를 매개변수에 넣어서 랜덤으로 가져올 수 있음.
    /// </summary>
    /// <param name="list">무작위 아이템을 뱉을 List 정의</param>
    /// <returns>List의 무작위 Item을 반환</returns>
    public Item GetRandomItem(List<ItemData> list)
    {
        return new Item(GetRandomItemData(list));
    }


    /// <summary>
    /// 확률에 따라 랜덤으로 등급을 지정받고 해당 등급에 따른 아이템을 반환하는 메서드
    /// </summary>
    /// <returns>특정 등급의 아이템을 반환</returns>
    public Item GetRandomRarityItem()
    {
        ItemRarity itemRarity = GetRandomItemRarity();
        switch(itemRarity)
        {
            case ItemRarity.Common:
                return new Item(GetRandomItemData(CommonDataList));

            case ItemRarity.Rare:
                return new Item(GetRandomItemData(RareDataList));

            case ItemRarity.SuperRare:
                return new Item(GetRandomItemData(SuperRareDataList));

            case ItemRarity.Epic:
                return new Item(GetRandomItemData(EpicDataList));

            case ItemRarity.Legend:
                return new Item(GetRandomItemData(LegendDataList));
        }

        Debug.LogError("itemRarity is null");
        return null;
    }
}
