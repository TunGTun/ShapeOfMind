using UnityEngine;

public abstract class CardColumnAbstract : MyBehaviour
{
    [Header("CardColumnAbstract")]

    [SerializeField] protected CardColumnCtrl cardColumnCtrl;
    public CardColumnCtrl CardColumnCtrl => cardColumnCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardColumnCtrl();
    }

    protected virtual void LoadCardColumnCtrl()
    {
        if (cardColumnCtrl != null) return;
        cardColumnCtrl = GetComponent<CardColumnCtrl>();
        Debug.LogWarning(transform.name + ": LoadCardColumnCtrl", gameObject);
    }
}
