using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    public float duration;
    public UnityAction actionProcess;

    public CanvasGroup canvasGroup;
    public Image Title;

    public Image Top;
    public Image Bottom;
    public void Initialize(float duration, UnityAction action)
    {
        this.duration = duration;
        this.actionProcess += action;
    }

    public void Start()
    {
        Begin();
    }

    public void Begin()
    {
        LeanTween.alphaCanvas(canvasGroup, 1, 0.25f).setEaseOutExpo().setOnComplete(()=>
        {gameObject.LeanCancel();
        Top.fillOrigin = 0;
        Bottom.fillOrigin = 1;
        gameObject.LeanValue(UpdateFill, 0, 1, duration * 0.4f ).setEaseInOutCirc().setIgnoreTimeScale(true);
        LeanTween.delayedCall(duration / 2, Process).setIgnoreTimeScale(true);
        }).setIgnoreTimeScale(true);
        
    }

    public void Process()
    {
        UpdateFill(1);
        actionProcess?.Invoke();
        End();
    }

    public void UpdateFill(float v)
    {
        Top.fillAmount = v;
        Bottom.fillAmount = v;
    }

    public void End()
    {
        gameObject.LeanCancel();
        Top.fillOrigin = 1;
        Bottom.fillOrigin = 0;
        gameObject.LeanValue(UpdateFill, 1, 0, duration * 0.4f ).setDelay(duration *  0.6f).setEaseInOutCirc().setOnComplete(() =>  
        LeanTween.alphaCanvas(canvasGroup, 0, 0.25f).setEaseOutExpo().setOnComplete(()=>
        Destroy(gameObject)).setIgnoreTimeScale(true)).setIgnoreTimeScale(true);
        
    }

    public static void LoadingScreen(UnityAction action, float duration = 0)
    {
        GameObject screen = GameObject.Instantiate(AssetsLoader.main.LoadingScreen);
        screen.GetComponent<LoadingScreenScript>().Initialize(duration, action);
        DontDestroyOnLoad(screen);
    }

}