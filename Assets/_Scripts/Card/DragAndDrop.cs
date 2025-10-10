using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : CardAbstract, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // trạng thái để rollback
    private Transform originalParent;
    private int originalSiblingIndex;
    private Vector2 originalAnchoredPos;
    private Vector2 originalPivot;

    // offset từ pivot -> center (tính bằng world space)
    private Vector3 pivotToCenterWorldOffset;

    // nếu bạn có CardColumnCtrl
    public CardColumnCtrl originalColumn;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
        originalAnchoredPos = this.cardCtrl.RectTransform.anchoredPosition;
        originalPivot = this.cardCtrl.RectTransform.pivot;
        originalColumn = GetComponentInParent<CardColumnCtrl>();

        // tắt raycast để UI slot nhận drop
        this.cardCtrl.CanvasGroup.blocksRaycasts = false;

        // --- Tính offset pivot->center ở world space (trước khi reparent!)
        Vector2 size = this.cardCtrl.RectTransform.rect.size;
        Vector2 pivot = this.cardCtrl.RectTransform.pivot;
        Vector2 pivotToCenterLocal = (new Vector2(0.5f, 0.5f) - pivot) * size;
        pivotToCenterWorldOffset = this.cardCtrl.RectTransform.TransformVector(pivotToCenterLocal);

        // reparent lên canvas (giữ world position)
        transform.SetParent(this.cardCtrl.Canvas.transform, true);
        transform.SetAsLastSibling();

        // đặt ngay tâm thẻ vào con trỏ
        if (TryGetWorldPoint(eventData, out Vector3 worldPoint))
        {
            transform.position = worldPoint - pivotToCenterWorldOffset;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (TryGetWorldPoint(eventData, out Vector3 worldPoint))
        {
            transform.position = worldPoint - pivotToCenterWorldOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // nếu không có slot nhận (vẫn là con của canvas) -> rollback
        if (transform.parent == this.cardCtrl.Canvas.transform)
        {
            transform.SetParent(originalParent, false);
            transform.SetSiblingIndex(originalSiblingIndex);
            this.cardCtrl.RectTransform.anchoredPosition = originalAnchoredPos;
            this.cardCtrl.RectTransform.pivot = originalPivot;
        }

        this.cardCtrl.CanvasGroup.blocksRaycasts = true;
    }

    private bool TryGetWorldPoint(PointerEventData eventData, out Vector3 worldPoint)
    {
        var canvas = this.cardCtrl.Canvas;
        if (canvas == null)
        {
            worldPoint = Vector3.zero;
            return false;
        }

        return RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out worldPoint
        );
    }
}
