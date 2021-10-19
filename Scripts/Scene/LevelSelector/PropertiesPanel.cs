using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesPanel : MonoBehaviour
{
    public static PropertiesPanel main;
    public Image RBackground;

    public TMP_Text Rname;
    public TMP_Text Rmusic;
    public TMP_Text Rdescription;
    public TMP_Text Rsource;

    public Image Rimage;

    public AudioSource audioSource;

    private void Awake()
    {
        main = this;
    }

    public void UpdatePanel(LevelData levelData)
    {
        levelData.LoadData(true);
        PlayAudio(levelData.musicClip);

        Rname.text = levelData.levelName;
        Rmusic.text = levelData.musicName;
        Rdescription.text = levelData.descriptions;
        Rsource.text = levelData.source;

        Rimage.sprite = levelData.levelSprite;
        RBackground.sprite = levelData.levelSprite;
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

}
