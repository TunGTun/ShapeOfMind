using UnityEngine;

public class CardAbstract : MyBehaviour
{
    [Header("CardColumnAbstract")]

    [SerializeField] protected CardCtrl cardCtrl;
    public CardCtrl CardCtrl => cardCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCardCtrl();
    }

    protected virtual void LoadCardCtrl()
    {
        if (cardCtrl != null) return;
        cardCtrl = GetComponent<CardCtrl>();
        Debug.LogWarning(transform.name + ": LoadCardCtrl", gameObject);
    }
}
