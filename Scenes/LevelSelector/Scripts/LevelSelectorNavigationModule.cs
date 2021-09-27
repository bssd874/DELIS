using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorNavigationModule : MonoBehaviour
{

    public static LevelSelectorNavigationModule main;

    void Awake()
    {
        main = this;
    }

    public void Back()
    {
        LoadingScreenScript.LoadingScreen(()=>SceneManager.LoadScene("MainMenu"), 1);
        
    }

    public void Play()
    {
        if(LevelSelectorScript._levelData.noteMaps.Length == 0) return;
        LoadingScreenScript.LoadingScreen(()=>SceneManager.LoadScene("Gameplay"), 1);
        
    }

}