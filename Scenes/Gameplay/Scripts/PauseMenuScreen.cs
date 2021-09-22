using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScreen : MonoBehaviour
{
    public static PauseMenuScreen main;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        GameplayState.main.Pause();
    }

    public void Finish()
    {
        Destroy(gameObject);
    }

    public void Resume()
    {
        GameplayState.main.Toggle();
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    public void Back()
    {
        GameplayState.main.UnPause();
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }
}
