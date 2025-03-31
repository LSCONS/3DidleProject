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
    private InventorySlot slot;
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
            icon.sprite = slot.item.Icon;  
            icon.enabled = true;
            quantityText.text = slot.quantity.ToString();
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        rectTransform.SetAsLastSibling(); // 맨 위로

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(slotIndex); // 위치 복구

        rectTransform.localPosition = Vector3.zero;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (draggedSlot != null && draggedSlot.slotIndex != this.slotIndex)
        {
            InventoryUI.Instance.inventory.MoveItem(draggedSlot.slotIndex, this.slotIndex);
            InventoryUI.Instance.Refresh();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slot.IsEmpty)
        {
            ItemTooltip.Instance.Show(slot.item);
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
            if (!slot.IsEmpty && slot.item.Type == ItemType.UseItem)
            {
                UseItem();
            }
        }
    }
    private void UseItem()
    {
        foreach (var use in slot.item.useItemDatas)
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
