using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesPanel : MonoBehaviour
{
    public static PropertiesPanel main;
    public Image RBackground;

    [Header("Data Reference")]
    public TMP_Text Rname;
    public TMP_Text Rmusic;
    public TMP_Text Rdescription;
    public TMP_Text Rsource;

    public AudioSource audioSource;

    private void Awake()
    {
        main = this;
    }

    public void UpdatePanel(LevelData levelData)
    {
        levelData.LoadData(true);
        PlayAudio(levelData.musicClip);

        RBackground.gameObject.LeanCancel();
        RBackground.transform.localScale = Vector3.one * 1.25f;
        RBackground.gameObject.LeanScale(Vector3.one, 0.5f).setEaseOutExpo();

        Rname.text = levelData.levelName;
        Rmusic.text = levelData.musicName;
        Rdescription.text = levelData.descriptions;
        Rsource.text = levelData.source;
        RBackground.sprite = levelData.levelSprite;

        AnimatePanels();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if (audioSource.isPlaying is false) audioSource.Play();
        float time = audioSource.time;

        audioSource.Pause();
        audioSource.clip = audioClip;
        audioSource.time = time % audioClip.length;

        audioSource.UnPause();
        if (audioSource.isPlaying is false) audioSource.Play();
    }

    public void AnimatePanels()
    {
        Rname.rectTransform.LeanCancel();
        Rname.rectTransform.LeanScaleX(1.2f, 0);
        Rname.rectTransform.LeanScaleX(1, 0.5f).setEaseOutExpo();

        Rmusic.rectTransform.LeanCancel();
        Rmusic.rectTransform.LeanScaleX(1.4f, 0);
        Rmusic.rectTransform.LeanScaleX(1, 0.5f).setEaseOutExpo();

        Rsource.rectTransform.LeanCancel();
        Rsource.rectTransform.LeanScaleX(1.1f, 0);
        Rsource.rectTransform.LeanScaleX(1, 0.5f).setEaseOutExpo();

        Rdescription.gameObject.LeanCancel();
        Rdescription.gameObject.LeanValue((Vector2 v) => Rdescription.rectTransform.anchorMin = v, new Vector2(0, 1), Vector2.zero, 1f).setEaseOutExpo();
    }

}
