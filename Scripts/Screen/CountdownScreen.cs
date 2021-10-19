using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScreen : MonoBehaviour
{
    [Header("Values")]
    public float duration = 0;
    public float timeLeft = 0;
    public Action action;


    [Header("References")]
    public TMP_Text Rcountdown;

    public void Countdown(float duration, Action action)
    {

        this.duration = duration;
        this.action = action;
        Begin();
    }

    public void Begin()
    {

        gameObject.SetActive(true);
        gameObject.LeanCancel();
        gameObject.LeanValue(UpdateTime, duration, 0, duration)
            .setOnComplete(() => End()).setIgnoreTimeScale(true);
    }

    public void UpdateTime(float t)
    {
        timeLeft = t;
        Rcountdown.text = String.Format("{0:00.00}", timeLeft);
    }

    public void End()
    {
        gameObject.LeanCancel();
        action?.Invoke();
        gameObject.SetActive(false);
    }
}
