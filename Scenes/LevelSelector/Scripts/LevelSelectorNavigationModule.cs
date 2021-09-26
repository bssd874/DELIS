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
        LoadingScreenScript.LoadingScreen(()=>SceneManager.LoadScene("Gameplay"), 1);
        
    }

}