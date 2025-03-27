//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Inventory : MonoBehaviour
//{
//    public InventorySlot[] slots;

//    public void Initialize(int slotCount)
//    {
//        slots = new InventorySlot[slotCount];
//        for (int i = 0; i < slotCount; i++)
//        {
//            slots[i] = new InventorySlot();
//        }
//    }

//    public void MoveItem(int fromIndex, int toIndex)
//    {
//        if (slots[fromIndex].IsEmpty) return;

//        if (slots[toIndex].IsEmpty)
//        {
//            slots[toIndex].Assign(slots[fromIndex].item, slots[fromIndex].quantity);
//            slots[fromIndex].Clear();
//        }
//        else
//        {
//            var temp = slots[toIndex];
//            slots[toIndex] = slots[fromIndex];
//            slots[fromIndex] = temp;
//        }
//    }
//}
