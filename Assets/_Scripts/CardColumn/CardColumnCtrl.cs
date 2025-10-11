using System.Linq;
using UnityEngine;

public class CardColumnCtrl : MyBehaviour
{
    [Header("CardColumnCtrl")]

    [SerializeField] protected CardSlot cardSlot;
    public CardSlot CardSlot => cardSlot;

    [SerializeField] protected CardStack cardStack;
    public CardStack CardStack => cardStack;

    [SerializeField] protected CardBoardCtrl cardBoardCtrl;
    public CardBoardCtrl CardBoardCtrl => cardBoardCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardSlot();
        this.LoadCardStack();
        this.LoadCardBoardCtrl();
    }

    protected virtual void LoadCardSlot()
    {
        if (cardSlot != null) return;
        cardSlot = GetComponent<CardSlot>();
        Debug.LogWarning(transform.name + ": LoadCardSlot", gameObject);
    }

    protected virtual void LoadCardStack()
    {
        if (cardStack != null) return;
        cardStack = GetComponent<CardStack>();
        Debug.LogWarning(transform.name + ": LoadCardStack", gameObject);
    }

    protected virtual void LoadCardBoardCtrl()
    {
        if (cardBoardCtrl != null) return;
        cardBoardCtrl = GetComponentInParent<CardBoardCtrl>();
        Debug.LogWarning(transform.name + ": LoadCardBoardCtrl", gameObject);
    }
}
