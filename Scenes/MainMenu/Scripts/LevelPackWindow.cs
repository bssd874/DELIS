using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LevelPackWindow : MonoBehaviour
{
    public LevelPack levelPack;

    [Header("References")]

    public TMP_Text Title;
    public TMP_Text Difficulty;

    public RectTransform LevelGrid;
    public GameObject LevelPanelObject;

    public void Initialize()
    {
        Title.text = levelPack.packName;
        Difficulty.text = levelPack.difficulty;

        foreach (LevelData levelData in levelPack.levelDatas)
        {
            GameObject panelObject = Instantiate(LevelPanelObject, LevelGrid);
            LevelPanelWindow panelWindow = panelObject.GetComponent<LevelPanelWindow>();
            panelWindow.levelData = levelData;
            panelWindow.Initialize();
        }
    }

    public void Select()
    {
        MainMenuScript.main.SelectLevelPack(levelPack);
    }

}
