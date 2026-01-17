using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private bool replaceIfOccupied = true;

    internal SlotItemView current; // unity inspectorda gözükmesin

    public bool IsOccupied => current != null;

    public void SetItem(SlotItemView view)
    {
        current = view;
    }

    public void Clear()
    {
        if (current) Destroy(current.gameObject);
        current = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!DragPayload.IsDragging) return;

        var panel = GetComponentInParent<UISlotPanel>();
        if (!panel) return;

        panel.TryDropToSlot(this, DragPayload.EnumValue);
        // drag manager EndDrag zaten pointerUp ile geliyor ama garanti:
        DragDropManager.I.CancelDrag();
    }

    public bool CanPlace()
    {
        return replaceIfOccupied || !IsOccupied;
    }
}
