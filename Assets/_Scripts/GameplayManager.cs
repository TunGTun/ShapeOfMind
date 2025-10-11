using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GameplayManager : MMSingleton<GameplayManager>
{
    [Header("Controller")]
    public ObjectPool ObjectPool;
    public CardBoardCtrl CardBoardController;
    
    [Header("Card Infos")]
    public List<CardCtrl> CardInfos;
}
