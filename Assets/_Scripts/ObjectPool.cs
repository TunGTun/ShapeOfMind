using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MyBehaviour
{
    private List<CardInfo> cardInfos = new List<CardInfo>();
    private Dictionary<CardInfo, Queue<CardInfo>> _cardPool = new Dictionary<CardInfo, Queue<CardInfo>>();
    
    protected override void Awake()
    {
        cardInfos = GameplayManager.Instance.CardInfos;
        foreach (var card in cardInfos)
        {
            InstantiateCards(card, 10);
        }
    }

    private void InstantiateCards(CardInfo cardInfo, int amount = 1)
    {
        if (!_cardPool.ContainsKey(cardInfo))
        {
            _cardPool.Add(cardInfo, new Queue<CardInfo>());
        }

        for (int i = 0; i < amount; i++)
        {
            var newCard = Instantiate(cardInfo, transform);
            newCard.gameObject.SetActive(false);
            _cardPool[cardInfo].Enqueue(newCard);
        }
    }

    public CardInfo GetCardInfo(CardInfo cardInfo)
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
    
    public CardInfo GetCardInfo(ECardColor cardColor, ECardForm cardForm)
    {
        CardInfo originalCardInfo = cardInfos.Find(c => c.CardColor == cardColor && c.CardForm == cardForm);
        
        if (originalCardInfo == null)
        {
            Debug.LogError("Card Info Not Found");
            return null;
        }

        return GetCardInfo(originalCardInfo);
    }

    public void ReturnToPool(CardInfo card)
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