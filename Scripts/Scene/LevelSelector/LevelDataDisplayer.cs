using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataDisplayer : MonoBehaviour
{
    public LevelPack levelPack;
    public GameObject levelDataPanelPrefab;
    public LevelDataPanel[] levelDataPanels;
    public GameObject levelDataSelectionGrid;

    [ContextMenu("Create Windows")]
    public void CreateLevelDataPanels()
    {
        foreach (LevelDataPanel window in levelDataPanels)
        {
            Destroy(window.gameObject);
        }

        LevelData[] levelDatas = levelPack.levelDatas;
        levelDataPanels = new LevelDataPanel[levelDatas.Length];

        for (int i = 0; i < levelDatas.Length; i++)
        {
            GameObject window = Instantiate(levelDataPanelPrefab, levelDataSelectionGrid.transform);
            LevelDataPanel levelDataPanel = window.GetComponent<LevelDataPanel>();
            levelDataPanel.levelData = levelDatas[i];

            levelDataPanel.Initialize();

            levelDataPanels[i] = levelDataPanel;
        }
    }
}
