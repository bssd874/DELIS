using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPackSelector : MonoBehaviour
{
    public static LevelPackSelector main;
    public GameObject levelPackWindowPrefab;
    public LevelPackWindow[] levelPackWindows;
    public GameObject levelPackSelectionGrid;

    private void Awake()
    {
        main = this;
    }

    [ContextMenu("Create Windowss")]
    public void CreateLevelPackWindows()
    {

        foreach (LevelPackWindow window in levelPackWindows)
        {
            Destroy(window.gameObject);
        }

        LevelPack[] levelPacks = MainMenu.levelPacks;
        levelPackWindows = new LevelPackWindow[levelPacks.Length];

        for (int i = 0; i < levelPacks.Length; i++)
        {
            GameObject window = Instantiate(levelPackWindowPrefab, levelPackSelectionGrid.transform);
            LevelPackWindow levelPackWindow = window.GetComponent<LevelPackWindow>();
            levelPackWindow.levelPack = levelPacks[i];

            levelPackWindow.Initialize();

            levelPackWindows[i] = levelPackWindow;
        }
    }
}
