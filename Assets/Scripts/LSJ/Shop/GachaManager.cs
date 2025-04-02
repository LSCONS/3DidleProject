using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class GachaManager : MonoBehaviour
{
    public static GachaManager Instance;

    public List<ItemData> gachaPool;
    public int gachaCost = 300;
    public GameObject panel;
    public Image rollingImage;         // 아이콘 보여줄 이미지
    public float rollingTime = 3f;     // 롤링 시간
    public float interval = 0.05f;     // 바뀌는 속도

    private void Awake()
    {
        Instance = this;
        gachaPool = new List<ItemData>();

        // 장비 아이템 불러오기
        LoadItemsFromPath("ItemData/EquipItem/Armor");
        LoadItemsFromPath("ItemData/EquipItem/Helmet");
        LoadItemsFromPath("ItemData/EquipItem/Necklace");
        LoadItemsFromPath("ItemData/EquipItem/Ring");
        LoadItemsFromPath("ItemData/EquipItem/Shoes");
        LoadItemsFromPath("ItemData/EquipItem/Weapon");

        // 소비 아이템 불러오기
        LoadItemsFromPath("ItemData/UseItem/PotionHP");
        LoadItemsFromPath("ItemData/UseItem/PotionMP");

        Debug.Log($"[GachaManager] 총 등록된 가챠 아이템 수: {gachaPool.Count}");
    }


    private void LoadItemsFromPath(string path)
    {
        ItemData[] items = Resources.LoadAll<ItemData>(path);
        gachaPool.AddRange(items);
    }

    public void PullGacha()
    {
        if (PlayerInventory.Instance.gold < gachaCost)
        {
            Debug.Log("골드 부족!");
            return;
        }

        PlayerInventory.Instance.gold -= gachaCost;
        StartCoroutine(RollEffectCoroutine());
    }

    private IEnumerator RollEffectCoroutine()
    {
        float timer = 0f;
        ItemData lastShown = null;

        while (timer < rollingTime)
        {
            lastShown = gachaPool[Random.Range(0, gachaPool.Count)];
            rollingImage.sprite = lastShown.Icon;
            timer += interval;
            yield return new WaitForSeconds(interval);
        }

        // 뽑힌 아이템 지급
        PlayerInventory.Instance.AddItem(lastShown, 1);
        InventoryUI.Instance.Refresh();
        // 결과창 띄우기
        GachaResultUI.Instance.Show(lastShown);
    }
    public void Close()
    {
        panel.SetActive(false);
    }
}
