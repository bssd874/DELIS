using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorScript : MonoBehaviour
{
    public static LevelSelectorScript main;

    public AudioSource audioSource;

    public LevelData currentLevelData;
    public PropertiesPanelScript propertiesPanel;
    
    void Awake()
    {
        main = this;
    }

    public void SetLevelData( LevelData levelData)
    {
        currentLevelData = levelData;
        propertiesPanel.SetLevelData(currentLevelData);
    }

    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Database.GameData.levelPack.UnloadAllMusic();
    }

}
