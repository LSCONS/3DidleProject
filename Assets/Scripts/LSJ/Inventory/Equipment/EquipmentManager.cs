using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    private Dictionary<EquipItemType, ItemData> equippedItems = new Dictionary<EquipItemType, ItemData>();

    private void Awake()
    {
        Instance = this;
    }

    public void Equip(EquipItemType type, ItemData newItem)
    {
        // 기존 장비가 있다면 인벤토리로 되돌리기
        if (equippedItems.TryGetValue(type, out var oldItem))
        {
            InventoryUI.Instance.inventory.AddItem(oldItem, 1);
        }

        equippedItems[type] = newItem;

        // (선택) 능력치 적용
        // PlayerStats.Instance.ApplyEquipStats(GetTotalStats());
    }

    public void Unequip(EquipItemType type)
    {
        if (equippedItems.TryGetValue(type, out var item))
        {
            InventoryUI.Instance.inventory.AddItem(item, 1);
            equippedItems.Remove(type);
        }

        EquipmentUI.Instance.Refresh();
    }

    public ItemData GetEquipped(EquipItemType type)
    {
        equippedItems.TryGetValue(type, out var item);
        return item;
    }

    public Dictionary<EquipItemType, ItemData> GetAllEquippedItems()
    {
        return equippedItems;
    }
}
