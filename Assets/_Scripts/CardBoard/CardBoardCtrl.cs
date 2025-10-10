using System.Linq;
using UnityEngine;

public class CardBoardCtrl : MyBehaviour
{
    [SerializeField] protected CardColumnCtrl[] cardColumnCtrls;
    public CardColumnCtrl[] CardColumnCtrls => cardColumnCtrls;

    [SerializeField] protected RectTransform boardTransform;
    public RectTransform BoardTransform => boardTransform;

    [SerializeField] protected BoardAutoResize boardAutoResize;
    public BoardAutoResize BoardAutoResize => boardAutoResize;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardColumnCtrls();
        this.LoadBoardTransform();
        this.LoadBoardAutoResize();
    }

    protected virtual void LoadCardColumnCtrls()
    {
        if (cardColumnCtrls.Count() > 0) return;
        cardColumnCtrls = GetComponentsInChildren<CardColumnCtrl>();
        Debug.LogWarning(transform.name + ": LoadCardColumnCtrls", gameObject);
    }

    protected virtual void LoadBoardTransform()
    {
        if (boardTransform != null) return;
        boardTransform = GetComponent<RectTransform>();
        Debug.LogWarning(transform.name + ": LoadBoardTransform", gameObject);
    }

    protected virtual void LoadBoardAutoResize()
    {
        if (boardAutoResize != null) return;
        boardAutoResize = GetComponent<BoardAutoResize>();
        Debug.LogWarning(transform.name + ": LoadBoardAutoResize", gameObject);
    }
}
