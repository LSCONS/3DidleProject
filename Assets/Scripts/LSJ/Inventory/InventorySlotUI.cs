using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class InventorySlotUI : MonoBehaviour,
   IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Slot Index")]
    public int slotIndex;

    [Header("UI Components")]
    public Image icon;
    public TMP_Text quantityText;

    private Transform originalParent;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public InventorySlot slot;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (icon == null || quantityText == null)
        {
            Debug.LogWarning($"[InventorySlotUI] ({gameObject.name}) UI 컴포넌트가 할당안됨 ");
        }
    }

    

    public void Set(InventorySlot slot)
    {
        this.slot = slot; 

        if (icon == null || quantityText == null) return;

        if (slot.IsEmpty)
        {
            icon.enabled = false;
            quantityText.text = "";
        }
        else
        {
            icon.sprite = slot.item.Data.Icon;  
            icon.enabled = true;
            quantityText.text = slot.quantity.ToString();
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // 슬롯은 그대로 두고 아이콘 복사만 보여줌
        if (!slot.IsEmpty)
        {
            DragIcon.Show(icon.sprite, canvas.transform);
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragIcon.Move(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragIcon.Hide();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //인벤토리 패널 밖이면 버리기 확인창 띄우기
        if (!RectTransformUtility.RectangleContainsScreenPoint(
            InventoryUI.Instance.slotParent.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera))
        {
            DropConfirmPanel.Instance.Show(this); 
        }
        else
        {
            InventoryUI.Instance.Refresh(); // 정상 슬롯 이동 시
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedSlot = eventData.pointerDrag;
        if (draggedSlot == null) return;

        InventorySlotUI inventorySlotUI = draggedSlot.GetComponent<InventorySlotUI>();

        if (inventorySlotUI != null && inventorySlotUI.slotIndex != this.slotIndex)
        {
            InventoryUI.Instance.inventory.MoveItem(inventorySlotUI.slotIndex, this.slotIndex);
            InventoryUI.Instance.Refresh();
            return;
        }

        if(draggedSlot.tag == "ItemUpgrade")
        {

        }

        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slot.IsEmpty)
        {
            ItemTooltip.Instance.Show(slot.item.Data);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemTooltip.Instance.Hide();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!slot.IsEmpty && slot.item.Data.Type == ItemType.UseItem)
            {
                UseItem();
            }
        }
    }
    private void UseItem()
    {
        foreach (var use in slot.item.Data.useItemDatas)
        {
            if (use.UseType == UseItemType.HP)
            {
                Debug.Log($"HP 회복: {use.HealthValue}");
            }
        }

        slot.quantity--;

        if (slot.quantity <= 0)
            slot.Clear();

        InventoryUI.Instance.Refresh();
    }
}
