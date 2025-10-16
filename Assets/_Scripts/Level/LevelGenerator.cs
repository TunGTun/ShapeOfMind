using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class LevelGenerator : MyBehaviour
{
    private List<CardCtrl> _cardInfos = new List<CardCtrl>();

    private int _cardForms = 6;
    private bool[,] _cardCheck;
    private List<(int, int)> _validSequence = new List<(int, int)>(); // color, form

    private int _seed = 264264;
    private System.Random _random;
    
    protected override void Awake()
    {
        _cardInfos = GameplayManager.Instance.CardInfos;
    }

    public void SetDataAndGenerateLevel(LevelSO levelData)
    {
        _seed = 264264 + levelData.levelID * levelData.cardColorAmount;
        _random = new System.Random(_seed);
        
        GenerateLevel(levelData.cardColorAmount);
    }

    private void GenerateLevel(int colorAmount = 3)
    {
        _cardCheck = new bool[colorAmount + 1, _cardForms + 1];
        _validSequence.Clear();

        List<int> cardForms = CreateShuffledList(_cardForms);
        List<int> cardColors = CreateShuffledList(colorAmount);

        int maxCount = _cardForms * colorAmount;
        
        if (!FindValidSequence(maxCount, cardForms, cardColors))
        {
            Debug.LogWarning("Failed to find valid sequence with current conditions");
        }
        else
        {
            Debug.Log($"Found valid sequence with {_validSequence.Count} cards");
            SpawnCardObjects();
            MatchCards();
            ArrangeAllCardColumns();
        }
    }

    private void SpawnCardObjects()
    {
        var cardColumns = GameplayManager.Instance.CardBoardController.CardColumnCtrls;

        foreach (var (color, form) in _validSequence)
        {
            var randomIndex = 0;
            while (true)
            {
                randomIndex = _random.Next(0, cardColumns.Length);

                if (((randomIndex == 0 || randomIndex == 3) && cardColumns[randomIndex].transform.childCount == 5) ||
                    ((randomIndex == 1 || randomIndex == 2) && cardColumns[randomIndex].transform.childCount == 4))
                {
                    continue;
                }

                break;
            }
            
            var newCard = GameplayManager.Instance.ObjectPool.GetCardInfo((ECardColor)color, (ECardForm)form);
            newCard.transform.SetParent(cardColumns[randomIndex].transform);
            newCard.ReloadComponents();
            newCard.transform.localScale = Vector3.one;
        }
    }

    private void MatchCards()
    {
        var cardColumns = GameplayManager.Instance.CardBoardController.CardColumnCtrls;
        
        foreach (var column in cardColumns)
        {
            var cardsInColumn = column.transform.GetComponentsInChildren<CardCtrl>();
            for (int i = 0; i < cardsInColumn.Length - 1; i++)
            {
                CardCtrl currentCard = cardsInColumn[i];
                CardCtrl nextCard = cardsInColumn[i + 1];
                if (currentCard.CardInfo.CardColor != nextCard.CardInfo.CardColor && CardFormCondition.IsValidFollow(currentCard.CardInfo.CardForm, nextCard.CardInfo.CardForm))
                {
                    nextCard.transform.SetParent(currentCard.transform);
                }
            }
        }
    }

    private void ArrangeAllCardColumns()
    {
        var cardColumns = GameplayManager.Instance.CardBoardController.CardColumnCtrls;

        foreach (var column in cardColumns)
        {
            column.ReloadComponents();
            column.CardStack.ReloadComponents();
            column.CardBoardCtrl.ReloadComponents();
        }
    }

    private List<int> CreateShuffledList(int count)
    {
        List<int> list = new List<int>(count);
        for (int i = 1; i <= count; i++)
        {
            list.Add(i);
        }
        
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
        
        return list;
    }

    private bool FindValidSequence(int maxCount, List<int> cardForms, List<int> cardColors)
    {
        if (_validSequence.Count == maxCount)
        {
            return IsValidClosedLoop();
        }

        for (int i = 0; i < cardColors.Count; i++)
        {
            int currentColor = cardColors[i];
            
            if (!CanUseColor(currentColor))
            {
                continue;
            }

            for (int j = 0; j < cardForms.Count; j++)
            {
                int currentForm = cardForms[j];
                
                if (!CanUseCard(currentColor, currentForm))
                {
                    continue;
                }

                AddCard(currentColor, currentForm);

                if (FindValidSequence(maxCount, cardForms, cardColors))
                {
                    return true;
                }

                RemoveCard(currentColor, currentForm);
            }
        }

        return false;
    }

    private bool CanUseColor(int color)
    {
        if (_validSequence.Count == 0)
        {
            return true;
        }
        
        return _validSequence[^1].Item1 != color;
    }

    private bool CanUseCard(int color, int form)
    {
        if (_cardCheck[color, form])
        {
            return false;
        }

        if (_validSequence.Count == 0)
        {
            return true;
        }

        ECardForm previousForm = (ECardForm)_validSequence[^1].Item2;
        ECardForm currentForm = (ECardForm)form;
        
        return CardFormCondition.IsValidFollow(previousForm, currentForm);
    }

    private bool IsValidClosedLoop()
    {
        ECardForm lastForm = (ECardForm)_validSequence[^1].Item2;
        ECardForm firstForm = (ECardForm)_validSequence[0].Item2;
        
        if (!CardFormCondition.IsValidFollow(lastForm, firstForm))
        {
            return false;
        }

        if (_validSequence[^1].Item1 == _validSequence[0].Item1)
        {
            return false;
        }

        return true;
    }

    private void AddCard(int color, int form)
    {
        _cardCheck[color, form] = true;
        _validSequence.Add((color, form));
    }

    private void RemoveCard(int color, int form)
    {
        _cardCheck[color, form] = false;
        _validSequence.RemoveAt(_validSequence.Count - 1);
    }

#if UNITY_EDITOR
    [ContextMenu("Print Valid Sequence")]
    private void PrintValidSequence()
    {
        if (_validSequence.Count == 0)
        {
            Debug.Log("Sequence not found!");
            return;
        }

        string result = "Valid Sequence:\n";
        for (int i = 0; i < _validSequence.Count; i++)
        {
            result += $"[{i}] Color: {_validSequence[i].Item1}, Form: {(ECardForm)_validSequence[i].Item2}\n";
        }
        Debug.Log(result);
    }
#endif
}