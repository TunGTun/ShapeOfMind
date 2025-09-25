using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : CardAbstract, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("DragAndDrop")]

    // ====== Trạng thái ban đầu để rollback nếu thả sai ======
    private Transform originalParent;
    private int originalSiblingIndex;
    private Vector2 originalAnchoredPos;

    //Optimaze
    public CardColumnCtrl originalColumn;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();
        originalAnchoredPos = this.cardCtrl.RectTransform.anchoredPosition;

        originalColumn = GetComponentInParent<CardColumnCtrl>();

        this.cardCtrl.CanvasGroup.blocksRaycasts = false;

        // Đưa card lên canvas để hiển thị trên cùng
        transform.SetParent(this.CardCtrl.Canvas.transform, true);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (TryGetLocalPoint(eventData, out Vector2 localPoint))
        {
            // Đặt lại vị trí theo con trỏ - offset để giữ đúng vị trí
            this.cardCtrl.RectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Nếu chưa có slot nào nhận (parent vẫn là canvas)
        if (transform.parent == this.CardCtrl.Canvas.transform)
        {
            // Rollback về trạng thái ban đầu
            transform.SetParent(originalParent, false);
            transform.SetSiblingIndex(originalSiblingIndex);
            this.cardCtrl.RectTransform.anchoredPosition = originalAnchoredPos;
        }

        // Bật lại raycast cho card
        this.CardCtrl.CanvasGroup.blocksRaycasts = true;
    }

    protected bool TryGetLocalPoint(PointerEventData eventData, out Vector2 localPoint)
    {
        return RectTransformUtility.ScreenPointToLocalPointInRectangle(
            this.CardCtrl.Canvas.transform as RectTransform,
            eventData.position,
            this.CardCtrl.Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : this.CardCtrl.Canvas.worldCamera,
            out localPoint
        );
    }
}
