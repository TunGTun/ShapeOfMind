using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CardCtrl : MyBehaviour
{
    [Header("CardCtrl")]

    [SerializeField] protected DragAndDrop dragAndDrop;
    public DragAndDrop DragAndDrop => dragAndDrop;

    [SerializeField] protected CardInfo cardInfo;
    public CardInfo CardInfo => cardInfo;

    [SerializeField] protected RectTransform rectTransform;
    public RectTransform RectTransform => rectTransform;

    [SerializeField] protected CanvasGroup canvasGroup;
    public CanvasGroup CanvasGroup => canvasGroup;

    [SerializeField] protected Canvas canvas;
    public Canvas Canvas => canvas;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDragAndDrop();
        this.LoadCardInfo();
        this.LoadRectTransform();
        this.LoadCanvasGroup();
        this.LoadCanvas();
    }

    protected virtual void LoadDragAndDrop()
    {
        if (dragAndDrop != null) return;
        dragAndDrop = GetComponent<DragAndDrop>();
        Debug.LogWarning(transform.name + ": LoadDragAndDrop", gameObject);
    }

    protected virtual void LoadCardInfo()
    {
        if (cardInfo != null) return;
        cardInfo = GetComponent<CardInfo>();
        Debug.LogWarning(transform.name + ": LoadCardInfo", gameObject);
    }

    protected virtual void LoadRectTransform()
    {
        if (rectTransform != null) return;
        rectTransform = GetComponent<RectTransform>();
        Debug.LogWarning(transform.name + ": LoadRectTransform", gameObject);
    }

    protected virtual void LoadCanvasGroup()
    {
        if (canvasGroup != null) return;
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.LogWarning(transform.name + ": LoadCanvasGroup", gameObject);
    }

    protected virtual void LoadCanvas()
    {
        if (canvas != null) return;
        canvas = GetComponentInParent<Canvas>();
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }
}
