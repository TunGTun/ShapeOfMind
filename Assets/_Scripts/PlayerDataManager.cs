using MoreMountains.Tools;
using UnityEngine;

public class PlayerDataManager : MMSingleton<PlayerDataManager>
{
    private const string PREF_CURRENT_LEVEL = "current_level";
    private const string PREF_MUSIC_ENABLED = "music_enabled";
    private const string PREF_SFX_ENABLED = "sfx_enabled";
    
    public int CurrentLevel
    {
        get
        {
            if (!PlayerPrefs.HasKey(PREF_CURRENT_LEVEL))
            {
                CurrentLevel = 1;
            }
            return PlayerPrefs.GetInt(PREF_CURRENT_LEVEL, 1);
        }
        set => PlayerPrefs.SetInt(PREF_CURRENT_LEVEL, value);
    }
}
