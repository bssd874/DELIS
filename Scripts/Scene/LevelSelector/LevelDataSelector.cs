using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataSelector : MonoBehaviour
{
    public static LevelDataSelector main;
    public GameObject levelDataWindowPrefab;
    public LevelDataWindow[] levelDataWindows;
    public GameObject levelDataSelectionGrid;

    private void Awake()
    {
        main = this;
    }

    [ContextMenu("Create Windowss")]
    public void CreateLevelDataWindows()
    {

        foreach (LevelDataWindow window in levelDataWindows)
        {
            Destroy(window.gameObject);
        }

        LevelData[] levelDatas = LevelSelector.levelPack.levelDatas;
        levelDataWindows = new LevelDataWindow[levelDatas.Length];

        for (int i = 0; i < levelDatas.Length; i++)
        {
            GameObject window = Instantiate(levelDataWindowPrefab, levelDataSelectionGrid.transform);
            LevelDataWindow levelDataWindow = window.GetComponent<LevelDataWindow>();
            levelDataWindow.levelData = levelDatas[i];

            levelDataWindow.Initialize();

            levelDataWindows[i] = levelDataWindow;
        }
    }
}
