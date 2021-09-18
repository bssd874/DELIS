using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public bool paused;

    public RectTransform background;
    public PauseScreenMenuWindow menuWindow;
    public AudioSource audioSource;

    public Text text;

    public static PauseScreen main;

    private void Awake()
    {
        main = this;
    }

    public void TogglePause()
    {
        SetPause(!paused);
    }

    public void Start()
    {
        GameplayData.WorldData.onUpdate.AddListener(UpdateText);
    }

    public void UpdateText()
    {
        text.text = GameplayData.WorldData.score.ToString();
    }

    public void SetPause(bool value)
    {
        paused = value;

        if (paused)
        {
            Pause();
        }
        else
        {
            UnPause();
        }
    }

    public void Pause()
    {
        audioSource.Pause();
        DimBackground(0.8f, 0.5f);
        Time.timeScale = 0;
        menuWindow.AnimateOpenMenu();
    }

    public void UnPause()
    {
        audioSource.UnPause();
        DimBackground(0, 0.5f);
        Time.timeScale = 1;
        menuWindow.AnimateCloseMenu();
    }

    public void DimBackground(float alpha, float time)
    {
        background.LeanCancel();
        background.LeanAlpha(alpha, time).setEaseOutExpo().setIgnoreTimeScale(true);
    }
}