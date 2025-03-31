using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public GameObject slotPrefab;
    public Transform slotParent;

    public Inventory inventory;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>(); // 🔥 추가

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize(Inventory targetInventory)
    {
        inventory = targetInventory;

        // 기존 UI 제거
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        slotUIs.Clear();

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            InventorySlotUI slotUI = go.GetComponent<InventorySlotUI>();
            slotUI.slotIndex = i;
            slotUI.Set(inventory.slots[i]);
            slotUIs.Add(slotUI); // 슬롯 UI 따로 저장
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < slotUIs.Count; i++)
        {
            slotUIs[i].Set(inventory.slots[i]);
        }
    }
}
