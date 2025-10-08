using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MyBehaviour
{
    private List<CardInfo> cardInfos = new List<CardInfo>();

    protected override void Awake()
    {
        cardInfos = GameplayManager.Instance.CardInfos;
    }
}
