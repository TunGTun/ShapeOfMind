using System.Diagnostics;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class ScenesManager : MMSingleton<ScenesManager>
{
    public enum SceneType
    {
        MainScene,
        GamePlay
    }
    public void LoadToScene(SceneType scene)
    {
        
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
        switch (scene)
        {
            case SceneType.MainScene:
                break;
            case SceneType.GamePlay:
                break;
            default:
                Debug.LogWarning("No music assigned for this scene: " + scene);
                break; 
        }
        Time.timeScale = 1;
    }
    //hàm riêng vì unity k cho găn onclick có tham số enum
    public void LoadMainScene()
    {
        LoadToScene(SceneType.MainScene);
    }

    public void LoadGamePlay()
    {
        LoadToScene(SceneType.GamePlay);
    }


    public void QuitGame()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }
}