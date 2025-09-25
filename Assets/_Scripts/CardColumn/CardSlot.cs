using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : CardColumnAbstract, IDropHandler
{
    //[Header("CardColumnCtrl")]
    public void OnDrop(PointerEventData eventData)
    {
        CardCtrl card = eventData.pointerDrag?.GetComponent<CardCtrl>();
        if (card == null ) return;

        CardColumnCtrl originalColumn = card.DragAndDrop.originalColumn;

        if (originalColumn.gameObject == this.gameObject) return;

        CardCtrl topCard = this.cardColumnCtrl.CardStack.TopCard;

        if (topCard != null)
        {
            // Điều kiện khác màu
            if (card.CardInfo.CardColor == topCard.CardInfo.CardColor)
            {
                Debug.Log("Trùng màu");
                return;
            }

            // Điều kiện form hợp lệ
            if (!CardFormCondition.IsValidFollow(topCard.CardInfo.CardForm, card.CardInfo.CardForm))
            {
                Debug.Log("Sai thứ tự form");
                return;
            }
        }

        originalColumn.CardStack.ArrangeCards();
        originalColumn.CardStack.LoadRaycastState();
        cardColumnCtrl.CardStack.AddCard(card);
    }
}
