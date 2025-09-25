using UnityEngine;

public class CardColumnCtrl : MyBehaviour
{
    [Header("CardColumnCtrl")]

    [SerializeField] protected CardSlot cardSlot;
    public CardSlot CardSlot => cardSlot;

    [SerializeField] protected CardStack cardStack;
    public CardStack CardStack => cardStack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardSlot();
        this.LoadCardStack();
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
}
