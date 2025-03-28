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
        inventoryUI.Initialize(inventory);
        inventory.AddItem(potionTest, 3);
    }
}
