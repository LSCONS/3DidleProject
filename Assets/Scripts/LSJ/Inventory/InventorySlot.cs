using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public bool IsEmpty => item == null || quantity <= 0;

    public void Clear()
    {
        item = null;
        quantity = 0;
    }

    public void Assign(ItemData newItem, int count)
    {
        item = newItem;
        quantity = count;
    }

    public void AddQuantity(int count)
    {
        quantity += count;
    }
}
