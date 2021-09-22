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
        SceneManager.LoadScene("MainMenu");
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

}