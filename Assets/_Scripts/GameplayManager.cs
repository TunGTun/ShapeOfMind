using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class GameplayManager : MMSingleton<GameplayManager>
{
    [Header("Controller")]
    public ObjectPool ObjectPool;
    public CardBoardCtrl CardBoardController;
    public LevelGenerator LevelGenerator;
    
    [Header("Card Infos")]
    public List<CardCtrl> CardInfos;

    private List<LevelSO> levelList = new List<LevelSO>();
    
    protected override void Awake()
    {
        GetAllLevelData();
    }

    private void Start()
    {
        SetLevel();
    }

    private void GetAllLevelData()
    {
        levelList.Clear();
        
        LevelSO[] levels = Resources.LoadAll<LevelSO>("Levels");
        levelList.Add(null);
        levelList.AddRange(levels);
    }

    private void SetLevel()
    {
        LevelGenerator.SetDataAndGenerateLevel(GetCurrentLevelData());
    }

    private LevelSO GetCurrentLevelData()
    {
        return levelList[PlayerDataManager.Instance.CurrentLevel];
    }
}
