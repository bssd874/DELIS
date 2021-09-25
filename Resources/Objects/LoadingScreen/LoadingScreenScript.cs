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
        LeanTween.alphaCanvas(canvasGroup, 1, 0.25f).setEaseOutSine().setOnComplete(()=>
        {gameObject.LeanCancel();
        Top.fillOrigin = 0;
        Bottom.fillOrigin = 1;
        gameObject.LeanValue(UpdateFill, 0, 1, duration / 2 ).setEaseInSine().setOnComplete(Process);});
        
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
        gameObject.LeanValue(UpdateFill, 1, 0, duration / 2 ).setEaseOutSine().setOnComplete(() =>  LeanTween.alphaCanvas(canvasGroup, 0, 0.25f).setEaseOutSine().setOnComplete(()=>Destroy(gameObject)));
        
    }

}