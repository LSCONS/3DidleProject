using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    public InventoryUI inventoryUI;
    public ItemData potionTest;
    public int slotCount = 20;

    private void Start()
    {
        inventory.Initialize(slotCount);
        inventory.AddItem(potionTest, 3);
        inventoryUI.Initialize(inventory);
    }
}
