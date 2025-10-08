using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GameplayManager : MMSingleton<GameplayManager>
{
    [Header("Controller")]
    public ObjectPool ObjectPool;
    
    [Header("Card Infos")]
    public List<CardInfo> CardInfos;
}
