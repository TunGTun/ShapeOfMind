using UnityEngine;

[CreateAssetMenu(fileName = "Level 000", menuName = "Game Data/Level Data")]
public class LevelSO : ScriptableObject
{
    [Header("Level Info")]
    public int levelID;
    
    [Header("Card Info")]
    public int cardColorAmount;
}
