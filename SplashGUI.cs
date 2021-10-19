using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashGUI : MonoBehaviour
{
    public AudioClip introMusic;
    public CanvasGroup SMK;
    public CanvasGroup KKSI;
    public CanvasGroup DELIS;
    public CanvasGroup TitleScreen;

    private void Start()
    {
        LeanAudio.play(introMusic);
        Time.timeScale = 1;
        LTSeq sequence = LeanTween.sequence();
        sequence.append(
            SMK.LeanAlpha(1, 0.25f)
        );
        sequence.append(3);
        sequence.append(
            SMK.LeanAlpha(0, 0.25f)
        );
        sequence.append(1);
        sequence.append(
            KKSI.LeanAlpha(1, 0.25f)
        );
        sequence.append(1);
        sequence.append(
            KKSI.LeanAlpha(0, 0.25f)
        );
        sequence.append(1);
        sequence.append(
            DELIS.LeanAlpha(1, 0.25f)
        );
        sequence.append(0.5f);
        sequence.append(
            TitleScreen.LeanAlpha(1, 0.25f)
        );
    }
}
