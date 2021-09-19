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

    public void SetLevelData(LevelData newLevelData)
    {
        levelData = newLevelData;
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        currentStage = currentStage % levelData.noteMaps.Length;

        noteMap = levelData.noteMaps[currentStage];

        

        AudioSource audioSource = LevelSelectorScript.main.audioSource;
        AudioClip audioClip = levelData.audioClip;

        print(audioClip);

        audioSource.time = audioSource.time % audioClip.length;
        audioSource.clip = audioClip;
        
        audioSource.Play();

        levelResult = noteMap.levelResult;
        Stage.text = currentStage.ToString();
        Rank.text = noteMap.levelResult.Rank;
        Title.text = levelData.levelName;
    }

    public void AddStage()
    {
        
        currentStage += 1;
        currentStage = currentStage % levelData.noteMaps.Length;
        UpdatePanel();
    }

}
