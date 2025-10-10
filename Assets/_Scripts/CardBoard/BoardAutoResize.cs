using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoardAutoResize : MyBehaviour
{
    [SerializeField] protected CardBoardCtrl cardBoardCtrl;
    public CardBoardCtrl CardBoardCtrl => cardBoardCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardBoardCtrl();
    }

    protected virtual void LoadCardBoardCtrl()
    {
        if (cardBoardCtrl != null) return;
        cardBoardCtrl = GetComponent<CardBoardCtrl>();
        Debug.LogWarning(transform.name + ": LoadCardBoardCtrl", gameObject);
    }

    public virtual void ResizeBoard()
    {
        if (this.cardBoardCtrl.CardColumnCtrls == null || this.cardBoardCtrl.CardColumnCtrls.Length == 0) return;

        float lowestY = float.MaxValue;
        bool found = false;

        foreach (CardColumnCtrl card in this.cardBoardCtrl.CardColumnCtrls)
        {
            if (card == null) continue;
            if (card.CardStack.TopCard == null) continue;
            found = true;

            Vector3 localPos = this.cardBoardCtrl.BoardTransform.parent.InverseTransformPoint(card.CardStack.TopCard.RectTransform.position);

            if (localPos.y < lowestY)
                lowestY = localPos.y;
        }

        if (found)
        {
            float newHeight = Mathf.Max(0 - lowestY + 150f, Data.boardMinHeight);
            Vector2 size = this.cardBoardCtrl.BoardTransform.sizeDelta;
            size.y = newHeight;
            this.cardBoardCtrl.BoardTransform.sizeDelta = size;

            LayoutRebuilder.ForceRebuildLayoutImmediate(this.cardBoardCtrl.BoardTransform.parent as RectTransform);
        }
    }
}
