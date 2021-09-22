using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayState : MonoBehaviour
{
    public static GameplayState main;

    public bool isPaused = false;
    public bool isCountdown = false;

    public RectTransform Screen;
    public Image TopIndicator;
    public Image BottomIndicator;
    
    public GameObject PauseMenuScreenObject;
    public GameObject CountdownScreenObject;

    void Start()
    {
        TopIndicator.gameObject.LeanAlpha(0, 1).setEaseLinear().setLoopPingPong();
    }

    void Update()
    {
        TopIndicator.fillAmount = (GameplayScript.main.audioSource.time / GameplayScript.main.audioSource.clip.length);
        BottomIndicator.fillAmount = TopIndicator.fillAmount;
        
    }

    void Awake()
    {
        main = this;
    }

    public void Toggle()
    {
        if (!isPaused || isCountdown)
        {
            DisplayPauseMenuScreen();
        }
        else
        {
            DisplayCountdownScreen();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        GameplayScript.main.audioSource.Pause();
        
    }
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1;
        GameplayScript.main.audioSource.UnPause();
        
    }
    public void DisplayPauseMenuScreen()
    {
        if (PauseMenuScreen.main) PauseMenuScreen.main.Finish();
        if (CountdownScreen.main) CountdownScreen.main.Cancel();
        Instantiate(PauseMenuScreenObject, Screen);
    }

    public void DisplayCountdownScreen()
    {
        if (PauseMenuScreen.main) PauseMenuScreen.main.Finish();
        if (CountdownScreen.main) CountdownScreen.main.Cancel();
        Instantiate(CountdownScreenObject, Screen);
    }

}
