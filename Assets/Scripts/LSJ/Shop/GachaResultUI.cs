using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaResultUI : MonoBehaviour
{
    public static GachaResultUI Instance;

    public GameObject panel;
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text specText;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(ItemData item)
    {
        icon.sprite = item.Icon;
        nameText.text = item.Name;
        descriptionText.text = item.Description;

        // 스펙 출력 예시
        if (item.Type == ItemType.EquipItem && item.equipItemData.Length > 0)
        {
            var data = item.equipItemData[0];
            specText.text = $"공격력: {data.AttackValue}\n방어력: {data.DefenceValue}";
        }
        else if (item.Type == ItemType.UseItem && item.useItemDatas.Length > 0)
        {
            var data = item.useItemDatas[0];
            specText.text = $"회복량: {data.HealthValue}";
        }
        else
        {
            specText.text = "기타 아이템";
        }

        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
