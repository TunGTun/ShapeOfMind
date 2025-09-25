using UnityEngine;

public class CardInfo : CardAbstract
{
    [SerializeField] protected ECardColor cardColor;
    public ECardColor CardColor => cardColor;

    [SerializeField] protected ECardForm cardForm;
    public ECardForm CardForm => cardForm;
}
