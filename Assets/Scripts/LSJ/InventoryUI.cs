//using UnityEngine;

//public class InventoryUI : MonoBehaviour
//{
//    public static InventoryUI Instance;

//    public GameObject slotPrefab;
//    public Transform slotParent;

//    public Inventory inventory;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    public void Initialize(Inventory targetInventory)
//    {
//        inventory = targetInventory;

//        for (int i = 0; i < inventory.slots.Length; i++)
//        {
//            GameObject go = Instantiate(slotPrefab, slotParent);
//            InventorySlotUI slotUI = go.GetComponent<InventorySlotUI>();
//            slotUI.slotIndex = i;
//            slotUI.Set(inventory.slots[i]);
//        }
//    }

//    public void Refresh()
//    {
//        for (int i = 0; i < slotParent.childCount; i++)
//        {
//            InventorySlotUI slotUI = slotParent.GetChild(i).GetComponent<InventorySlotUI>();
//            slotUI.Set(inventory.slots[i]);
//        }
//    }
//}
