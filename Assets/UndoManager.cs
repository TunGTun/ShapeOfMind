using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class MoveAction
{
    public CardCtrl card;             // Thẻ được di chuyển
    public CardColumnCtrl fromColumn; // Cột gốc
    public CardColumnCtrl toColumn;   // Cột đích
    public int fromChildIndex;        // Vị trí trong stack trước khi rời đi
}

public class UndoManager : MMSingleton<UndoManager>
{
    private Stack<MoveAction> history = new Stack<MoveAction>();

    public void RecordMove(CardCtrl card, CardColumnCtrl from, CardColumnCtrl to, int fromIndex)
    {
        history.Push(new MoveAction
        {
            card = card,
            fromColumn = from,
            toColumn = to,
            fromChildIndex = fromIndex
        });
    }

    public void Undo()
    {
        if (history.Count == 0) return;

        MoveAction last = history.Pop();

        // Di chuyển ngược lại
        last.card.transform.SetParent(last.fromColumn.CardStack.transform, false);

        // Có thể cần phục hồi vị trí chính xác
        RectTransform cardRect = last.card.RectTransform;
        cardRect.anchoredPosition = new Vector2(0, last.fromChildIndex * Data.childOffsetY);

        // Cập nhật lại hiển thị
        last.fromColumn.CardStack.ArrangeCards();
        last.toColumn.CardStack.ArrangeCards();

        last.toColumn.CardBoardCtrl.BoardAutoResize.ResizeBoard();

        Data.StepCount--;
    }
}