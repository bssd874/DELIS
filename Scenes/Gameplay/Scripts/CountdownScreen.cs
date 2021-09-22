using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class CountdownScreen : MonoBehaviour
{
    public TMP_Text textCount;
    public RectTransform container;

    public float countdown = 4;

    public static CountdownScreen main;

    Coroutine countdownCoroutine;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        countdownCoroutine = StartCoroutine(StartCountdown());
    }

    public void Cancel()
    {
        Destroy(gameObject);
        GameplayState.main.isCountdown = false;

    }

    public void Finish()
    {
        Destroy(gameObject);
        GameplayState.main.UnPause();
    }

    IEnumerator StartCountdown()
    {
        GameplayState.main.isCountdown = true;
        container.LeanSize(new Vector2(2000, 150), countdown).setIgnoreTimeScale(true);
        while (countdown > 0)
        {
            countdown -= Time.unscaledDeltaTime;
            textCount.text = string.Format("{0:0.00}", Mathf.Clamp(countdown, 0, Mathf.Infinity)); ;
            yield return null;
        }
        Finish();
        GameplayState.main.isCountdown = false;
        yield return null;
    }

}
