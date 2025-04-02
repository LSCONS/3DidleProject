using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;

    public void Initialize(int slotCount)
    {
        slots = new InventorySlot[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = new InventorySlot();
        }
    }

    public void MoveItem(int fromIndex, int toIndex)
    {
        if (slots[fromIndex].IsEmpty) return;

        if (slots[toIndex].IsEmpty)
        {
            slots[toIndex].Assign(slots[fromIndex].item, slots[fromIndex].quantity);
            slots[fromIndex].Clear();
        }
        else
        {
            Item tempItem = slots[toIndex].item;
            int tempQty = slots[toIndex].quantity;

            slots[toIndex].Assign(slots[fromIndex].item, slots[fromIndex].quantity);
            slots[fromIndex].Assign(tempItem, tempQty);
        }
    }
    public void SortByRarityDescending()
    {
        List<InventorySlot> slotList = new List<InventorySlot>(slots);

        // 비어 있지 않은 슬롯들만 대상으로 정렬
        slotList.Sort((a, b) =>
        {
            if (a.IsEmpty && b.IsEmpty) return 0;
            if (a.IsEmpty) return 1;
            if (b.IsEmpty) return -1;
            return b.item.Data.Rarity.CompareTo(a.item.Data.Rarity);
        });

        // 정렬된 결과를 다시 배열에 반영
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < slotList.Count)
            {
                slots[i] = slotList[i];
            }
            else
            {
                slots[i] = new InventorySlot(); // 빈 슬롯
            }
        }
    }
    public bool AddItem(Item item, int amount)
    {
        // 스택 가능한 아이템이면 기존 슬롯에 추가
        if (item.Data.IsStack)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == item && slots[i].quantity < item.Data.MaxStack)
                {
                    slots[i].AddQuantity(amount);
                    return true;
                }
            }
        }

        // 빈 슬롯에 새로 할당
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].Assign(item, amount);
                return true;
            }
        }

        // 인벤토리에 빈 공간 없음
        Debug.Log("인벤토리에 공간이 부족합니다.");
        return false;
    }
}
