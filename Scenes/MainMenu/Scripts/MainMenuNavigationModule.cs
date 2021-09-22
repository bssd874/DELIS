using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigationModule : MonoBehaviour
{
    public static MainMenuNavigationModule main;

    void Awake()
    {
        main = this;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Shop()
    {

    }

    public void Settings()
    {

    }

}