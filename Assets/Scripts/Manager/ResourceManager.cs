using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public ItemData[] ArmorItemDatas;
    public ItemData[] HelmetItemDatas;
    public ItemData[] NecklaceItemDatas;
    public ItemData[] RingItemDatas;
    public ItemData[] ShoesItemDatas;
    public ItemData[] WeaponItemDatas;
    public ItemData[] PotionHPItemDatas;
    public ItemData[] PotionMPItemDatas;

    public List<ItemData> AllItemDataList;

    private string equipItemPath = "ItemData/EquipItem";
    private string useItemPath = "ItemData/UseItem";

    protected override void Awake()
    {
        base.Awake();
        InitDatas();
        InitAllItemList();
    }


    //Resource의 데이터를 모두 통합
    private void InitDatas()
    {
        ArmorItemDatas = Resources.LoadAll<ItemData>(equipItemPath + "/Armor");
        HelmetItemDatas = Resources.LoadAll<ItemData>(equipItemPath + "/Helmet");
        NecklaceItemDatas = Resources.LoadAll<ItemData>(equipItemPath + "/Necklace");
        RingItemDatas = Resources.LoadAll<ItemData>(equipItemPath + "/Ring");
        ShoesItemDatas = Resources.LoadAll<ItemData>(equipItemPath + "/Shoes");
        WeaponItemDatas = Resources.LoadAll<ItemData>(equipItemPath + "/Weapon");
        PotionHPItemDatas = Resources.LoadAll<ItemData>(useItemPath + "/PotionHP");
        PotionMPItemDatas = Resources.LoadAll<ItemData>(useItemPath + "/PotionMP");
    }


    //AllItemList에게 모든 아이템 통합
    private void InitAllItemList()
    {
        AddRangeAllItemList(ArmorItemDatas);
        AddRangeAllItemList(HelmetItemDatas);
        AddRangeAllItemList(NecklaceItemDatas);
        AddRangeAllItemList(RingItemDatas);
        AddRangeAllItemList(ShoesItemDatas);
        AddRangeAllItemList(WeaponItemDatas);
        AddRangeAllItemList(PotionHPItemDatas);
        AddRangeAllItemList(PotionMPItemDatas);
    }


    //AllItemList를 추가하는 메서드
    private void AddRangeAllItemList(ItemData[] datas)
    {
        if(datas != null) AllItemDataList.AddRange(datas);
    }


    // 랜덤으로 ItemData을 반환하는 메서드
    private ItemData GetRandomItemData()
    {
        int rand = Random.Range(0, AllItemDataList.Count);
        return Instantiate(AllItemDataList[rand]);
    }

    
    /// <summary>
    /// 랜덤으로 Item을 반환하는 메서드
    /// </summary>
    /// <returns>무작위 Item을 반환</returns>
    public Item GetRandomItem()
    {
        return new Item(GetRandomItemData());
    }
}
