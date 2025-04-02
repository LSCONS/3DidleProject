using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance;

    public GameObject slotPrefab;
    public Transform slotParent;

    private void Awake()
    {
        Instance = this;
    }

    public void Refresh()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (var item in ShopManager.Instance.currentItems)
        {
            var go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<ShopItemSlotUI>();
            slot.Set(item);
        }
    }
}
