using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataSelector : MonoBehaviour
{
    public GameObject LevelDataPanelObject;
    public RectTransform Grid;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        LevelData[] levelDatas = Database.GameData.currentLevelDatas;

        foreach (LevelData levelData in levelDatas)
        {
            GameObject panelObject = Instantiate(LevelDataPanelObject, Grid);
            LevelDataPanel panelWindow = panelObject.GetComponent<LevelDataPanel>();
            panelWindow.levelData = levelData;
            panelWindow.Initialize();
        }
    }
}
