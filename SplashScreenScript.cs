using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
    public CanvasGroup KKSI;
    public CanvasGroup SMK;
    public CanvasGroup TitleScreen;

    
    public void Start()
    {
        Splash();
    }

    public void Splash()
    {
        TitleScreen.interactable =false;
        LTSeq sequence = LeanTween.sequence();
        sequence.append(2);
        sequence.append(KKSI.LeanAlpha(1, 0.5f).setEaseInSine());
        sequence.append(1);
        sequence.append(KKSI.LeanAlpha(0, 0.5f).setEaseInSine());
        sequence.append(()=>KKSI.gameObject.SetActive(false));
        sequence.append(1);
        sequence.append(SMK.LeanAlpha(1, 0.5f).setEaseInSine());
        sequence.append(1);
        sequence.append(SMK.LeanAlpha(0, 0.5f).setEaseInSine());
        sequence.append(()=>SMK.gameObject.SetActive(false));
        sequence.append(2);
        
        TitleScreen.gameObject.LeanScale(Vector3.one * 0.8f, 0);
        sequence.append(TitleScreen.LeanAlpha(1, 1.5f).setEaseOutSine());
        sequence.insert(TitleScreen.gameObject.LeanScale(Vector3.one, 1.5f).setEaseOutExpo());
        sequence.append(()=>TitleScreen.interactable = true);
    }

    public void Play()
    {
        LoadingScreenScript.LoadingScreen(()=>UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"), 1);
    }

}
