using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public List<ItemData> allShopItems;    // 전체 상점 아이템 목록
    public List<ItemData> currentItems;    // 현재 상점에 표시될 아이템
    public int itemCount = 3;              // 한 번에 표시할 아이템 수

    public float refreshInterval = 600f;   // 자동 갱신 시간 (초)
    private float timer;

    private void Awake()
    {
        Instance = this;
        ItemData[] equipItems = Resources.LoadAll<ItemData>("ItemData/EquipItem");
        ItemData[] useItems = Resources.LoadAll<ItemData>("ItemData/UseItem");

        // 하나의 리스트에 통합
        allShopItems = new List<ItemData>();
        allShopItems.AddRange(equipItems);
        allShopItems.AddRange(useItems);
    }

    private void Start()
    {
        RefreshShop();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= refreshInterval)
        {
            RefreshShop();
            timer = 0;
        }
    }

    public void RefreshShop()
    {
        currentItems.Clear();

        var candidates = allShopItems.FindAll(item => item.AvailableInShop);

        if (candidates.Count == 0)
        {
            Debug.LogWarning("상점에 등장 가능한 아이템이 없습니다!");
            return;
        }

        int spawnCount = Mathf.Min(itemCount, candidates.Count);

        //중복 없는 랜덤 아이템 선택
        List<ItemData> shuffled = new List<ItemData>(candidates);
        shuffled = shuffled.OrderBy(x => Random.value).ToList(); 
        currentItems = shuffled.Take(spawnCount).ToList();

        ShopUI.Instance.Refresh();
    }



    public void BuyItem(ItemData item)
    {
        if (PlayerInventory.Instance.gold >= item.ShopPrice)
        {
            PlayerInventory.Instance.gold -= item.ShopPrice;
            PlayerInventory.Instance.AddItem(item, 1);
            Debug.Log($"구매 성공: {item.Name}");
        }
        else
        {
            Debug.Log("골드 부족!");
        }
    }
}
