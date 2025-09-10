using UnityEngine;

public class CardStack : CardColumnAbstract
{
    [Header("CardStack")]

    [SerializeField] protected CardCtrl topCard;
    public CardCtrl TopCard => topCard;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.ArrangeCards();
        this.LoadRaycastState();
    }

    public void AddCard(CardCtrl card)
    {
        //Chưa tối ưu chỗ while
        //this.LoadTopCard();

        RectTransform cardRect = card.RectTransform;

        if (transform.childCount == 0)
        {
            card.transform.SetParent(transform, false);
            cardRect.anchoredPosition = Vector2.zero;
        }
        else
        {
            Transform parentForNewCard = TopCard.transform;
            card.transform.SetParent(parentForNewCard, false);
            cardRect.anchoredPosition = new Vector2(0, Data.childOffsetY);
        }

        //Chưa tối ưu chỗ for và gọi lại LoadTopCard()
        this.ArrangeCards();
    }

    private void LoadTopCard()
    {
        if (transform.childCount == 0)
        {
            topCard = null;
            return;
        }

        Transform lastBaseCard = transform.GetChild(transform.childCount - 1);

        Transform current = lastBaseCard;
        while (current.childCount > 0)
        {
            current = current.GetChild(current.childCount - 1);
        }

        topCard = current.GetComponent<CardCtrl>();
    }

    public void ArrangeCards()
    {
        float startY = Data.globalOffsetY + Data.childOffsetY;

        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform baseCard = transform.GetChild(i).GetComponent<RectTransform>();

            // Đặt vị trí cho card ngang hàng
            baseCard.anchoredPosition = new Vector2(0, startY);

            // Sắp xếp đệ quy cho tất cả con của nó
            float endY = ArrangeChildCards(baseCard, startY);

            // Di chuyển xuống dưới cho card ngang hàng tiếp theo
            startY = endY + Data.baseOffsetY;
        }

        LoadTopCard(); // cập nhật lại topCard sau khi sắp xếp
    }

    private float ArrangeChildCards(RectTransform parentCard, float parentY)
    {
        if (parentCard.childCount == 0)
            return parentY;

        float currentY = Data.childOffsetY;
        float lastChildEndY = parentY;

        for (int i = 0; i < parentCard.childCount; i++)
        {
            RectTransform childCard = parentCard.GetChild(i).GetComponent<RectTransform>();

            childCard.anchoredPosition = new Vector2(0, currentY);

            // Đệ quy nếu childCard cũng có con
            float endY = ArrangeChildCards(childCard, parentY + currentY);

            lastChildEndY = endY;

            currentY += Data.childOffsetY;
        }

        return lastChildEndY;
    }

    public void LoadRaycastState()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            CardCtrl card = transform.GetChild(i).GetComponent<CardCtrl>();
            if (card == null || card.CanvasGroup == null) continue;

            card.CanvasGroup.blocksRaycasts = (i == count - 1); // chỉ bật cho con cuối
        }
    }
}
