using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    public ItemData item;
    public int price;
}
