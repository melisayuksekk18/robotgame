using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaletteItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private controller.State enumValue;
    [SerializeField] private Image icon;

    public controller.State Value => enumValue;

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragDropManager.I.BeginDrag(this, enumValue, icon ? icon.sprite : null);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ghost update manager'da
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDropManager.I.EndDrag();
    }
}
