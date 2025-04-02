using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    public InventoryUI inventoryUI;
    public ItemData potionTest;
    public ItemData armorTest;
    public ItemData shoeTest;
    public int slotCount = 20;
    private void Awake()
    {
        if (inventory == null)
            inventory = GetComponent<Inventory>();

        if (inventoryUI == null)
            inventoryUI = FindObjectOfType<InventoryUI>();
    }
    private void Start()
    {
        inventory.Initialize(slotCount);
        inventory.AddItem(potionTest, 3);
        inventory.AddItem(armorTest, 1);
        inventory.AddItem(shoeTest, 1);
        inventoryUI.Initialize(inventory);
    }
}
