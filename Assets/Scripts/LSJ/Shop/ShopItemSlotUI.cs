using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public Button buyButton;

    private ItemData item;

    public void Set(ItemData data)
    {
        item = data;
        icon.sprite = data.Icon;
        nameText.text = data.Name;
        priceText.text = $"{data.ShopPrice} G";

        buyButton.onClick.RemoveAllListeners(); // 이전 이벤트 제거
        buyButton.onClick.AddListener(Buy);
    }

    private void Buy()
    {
        ShopManager.Instance.BuyItem(item);
        ShopUI.Instance.Refresh(); // 선택적으로 갱신
    }
}
