using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public CountdownScreen SCountdown;
    public void Pause()
    {
        gameObject.SetActive(true);
        Gameplay.main.Freeze();
    }
    public void UnPause()
    {
        Gameplay.main.UnFreeze();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        SCountdown.Countdown(3, UnPause);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector");
    }
}
