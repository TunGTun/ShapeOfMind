using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MyBehaviour
{
    private List<CardCtrl> _cardInfos = new List<CardCtrl>();
    private Dictionary<CardCtrl, Queue<CardCtrl>> _cardPool = new Dictionary<CardCtrl, Queue<CardCtrl>>();
    
    protected override void Awake()
    {
        _cardInfos = GameplayManager.Instance.CardInfos;
        foreach (var card in _cardInfos)
        {
            InstantiateCards(card, 10);
        }
    }

    private void InstantiateCards(CardCtrl cardInfo, int amount = 1)
    {
        if (!_cardPool.ContainsKey(cardInfo))
        {
            _cardPool.Add(cardInfo, new Queue<CardCtrl>());
        }

        for (int i = 0; i < amount; i++)
        {
            var newCard = Instantiate(cardInfo, transform);
            newCard.gameObject.SetActive(false);
            _cardPool[cardInfo].Enqueue(newCard);
        }
    }

    public CardCtrl GetCardInfo(CardCtrl cardInfo)
    {
        if (!_cardPool.ContainsKey(cardInfo))
        {
            Debug.LogError("Card Info Not Found");
            return null;
        }

        if (_cardPool[cardInfo].Count == 0)
        {
            InstantiateCards(cardInfo);
        }

        var card = _cardPool[cardInfo].Dequeue();
        card.gameObject.SetActive(true);
        card.transform.SetParent(null);
        return card;
    }
    
    public CardCtrl GetCardInfo(ECardColor cardColor, ECardForm cardForm)
    {
        CardCtrl originalCardInfo = _cardInfos.Find(c => c.CardInfo.CardColor == cardColor && c.CardInfo.CardForm == cardForm);
        
        if (originalCardInfo == null)
        {
            Debug.LogError("Card Info Not Found");
            return null;
        }

        return GetCardInfo(originalCardInfo);
    }

    public void ReturnToPool(CardCtrl card)
    {
        if (card == null)
        {
            Debug.LogWarning("Cannot return null card to pool");
            return;
        }

        card.gameObject.SetActive(false);
        card.transform.SetParent(transform);
        
        if (card != null && _cardPool.ContainsKey(card))
        {
            _cardPool[card].Enqueue(card);
        }
        else
        {
            Debug.LogWarning("Card type not found in pool, destroying card");
            Destroy(card.gameObject);
        }
    }
}