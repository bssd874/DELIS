using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PropertiesPanelScript : MonoBehaviour
{
    public LevelData levelData;
    public LevelResult levelResult;
    public int currentStage = 0;

    public NoteMap noteMap;

    [Header("References")]
    public TMP_Text Title;
    public TMP_Text Stage;
    public TMP_Text Rank;



    AudioSource audioSource;


    public void SetLevelData(LevelData newLevelData)
    {
        levelData = newLevelData;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        UpdateNoteMap();
        UpdateStage();
        UpdateRank();
        UpdateInfo();
        PlayAudio();
    }

    public void UpdateRank()
    {
        Rank.text = noteMap.levelResult.rank.ToString();
    }

    public void UpdateNoteMap()
    {
        noteMap = levelData.noteMaps[currentStage];
        levelResult = noteMap.levelResult;
    }

    public void UpdateStage()
    {
        currentStage = currentStage % levelData.noteMaps.Length;
        Stage.text = currentStage.ToString();
    }

    public void UpdateInfo()
    {
        Title.text = levelData.levelName;
    }

    public void PlayAudio()
    {
        audioSource = LevelSelectorScript.main.audioSource;
        AudioClip audioClip = levelData.audioClip;

        if (audioSource.clip != audioClip)
        {
            audioSource.time = audioSource.time % audioClip.length;
            audioSource.clip = audioClip;
            
            audioSource.Play();
        }
    }

    public void AddStage()
    {
        
        currentStage += 1;
        currentStage = currentStage % levelData.noteMaps.Length;
        UpdatePanel();
    }

}
