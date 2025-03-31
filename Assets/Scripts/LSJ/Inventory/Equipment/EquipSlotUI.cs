using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlotUI : MonoBehaviour, IDropHandler
{
    public EquipItemType slotType; // 이 슬롯의 타입 (Helmet, Weapon 등)
    public Image icon;

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (draggedSlot == null || draggedSlot.slot.IsEmpty) return;

        var item = draggedSlot.slot.item;

        if (item.Type != ItemType.EquipItem) return;

        // 해당 슬롯에 장착 가능한지 확인
        foreach (var equipData in item.equipItemData)
        {
            if (equipData.EquipType == slotType)
            {
                // 장착 처리
                EquipmentManager.Instance.Equip(slotType, item);
                draggedSlot.slot.Clear();
                InventoryUI.Instance.Refresh();
                EquipmentUI.Instance.Refresh();
                return;
            }
        }

        Debug.Log("이 슬롯에 장착할 수 없는 아이템입니다.");
    }

    public void Set(ItemData item)
    {
        if (item == null)
        {
            icon.enabled = false;
        }
        else
        {
            icon.sprite = item.Icon;
            icon.enabled = true;
        }
    }
}
