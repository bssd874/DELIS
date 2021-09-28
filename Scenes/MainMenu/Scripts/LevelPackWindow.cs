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
    public TMP_Text Cost;

    public RectTransform LevelGrid;
    public GameObject LevelPanelObject;
    public GameObject LockScreen;

    public void Initialize()
    {
        Title.text = levelPack.packName;
        Cost.text = levelPack.levelPackInfo.cost.ToString();

        foreach (LevelData levelData in levelPack.levelDatas)
        {
            GameObject panelObject = Instantiate(LevelPanelObject, LevelGrid);
            LevelPanelWindow panelWindow = panelObject.GetComponent<LevelPanelWindow>();
            panelWindow.levelData = levelData;
            panelWindow.Initialize();
        }

        UpdateLockScreen();
    }

    public void Unlock()
    {
        if (!levelPack._LevelPackData.unlocked)
        {
            int cost = levelPack.levelPackInfo.cost;
            if (Database.User._data.JPoints >= cost)
            {
                Database.User._data.JPoints -= cost;
                levelPack._LevelPackData.unlocked = true;
            }
        }
        UpdateLockScreen();
        levelPack.SaveData();
    }

    void UpdateLockScreen()
    {
        if (levelPack._LevelPackData.unlocked) LockScreen.SetActive(false);
    }

    public void Select()
    {
        MainMenuScript.main.SelectLevelPack(levelPack);
    }

}
