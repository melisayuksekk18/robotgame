using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropManager : MonoBehaviour
{
    public static DragDropManager I;

    [Header("Ghost")]
    [SerializeField] private Canvas overlayCanvas;          // Overlay canvas
    [SerializeField] private RectTransform ghostPrefab;     // basit bir Image + Layout vs.

    private void Awake()
    {
        I = this;
        if (!overlayCanvas) overlayCanvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (!DragPayload.IsDragging || DragPayload.Ghost == null) return;

        // Overlay canvas için mouse pozisyonunu direkt kullan
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)overlayCanvas.transform,
            Input.mousePosition,
            overlayCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : overlayCanvas.worldCamera,
            out pos
        );

        DragPayload.Ghost.anchoredPosition = pos;
    }

    public void BeginDrag(PaletteItem source, controller.State value, Sprite icon = null)
    {
        CancelDrag(); // varsa temizle

        DragPayload.IsDragging = true;
        DragPayload.EnumValue = value;
        DragPayload.Source = source;

        var g = Instantiate(ghostPrefab, overlayCanvas.transform);
        DragPayload.Ghost = g;

        // İstersen ghost ikonunu set et
        var img = g.GetComponentInChildren<UnityEngine.UI.Image>();
        if (img && icon) img.sprite = icon;

        // Raycast blocklamasın (drop alanlarına engel olmasın)
        var cg = g.GetComponent<CanvasGroup>();
        if (!cg) cg = g.gameObject.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;
        cg.interactable = false;
    }

    public void EndDrag()
    {
        CancelDrag();
    }

    public void CancelDrag()
    {
        if (DragPayload.Ghost) Destroy(DragPayload.Ghost.gameObject);

        DragPayload.IsDragging = false;
        DragPayload.Ghost = null;
        DragPayload.Source = null;
    }
}
