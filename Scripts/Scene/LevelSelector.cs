using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector main;
    public static LevelPack levelPack;
    public static LevelData levelData;
    public LevelPack _levelPack;

    public static void Enter(LevelPack levelPack = null)
    {
        if (LevelSelector.levelPack is null) LevelSelector.levelPack = levelPack;

        LevelSelector.levelPack.LoadMusics();
        SceneManager.LoadScene("LevelSelector");
    }

    private void Awake()
    {
        main = this;
        if (levelPack is null) levelPack = _levelPack;
    }

    private void Start()
    {
        LevelDataSelector.main.CreateLevelDataWindows();

        if (levelData) Select(levelData);
        else Select(levelPack.levelDatas[0]);
    }

    public void Select(LevelData level)
    {
        levelData = level;
        PropertiesPanel.main.UpdatePanel(level);
        UserStats.main.UpdateStats(level);
    }

    public void Play()
    {
        LoadingScreen.Load(() =>
        {
            if (Game.player.creator)
            {
                NoteMapCreator.Create(levelData);
            }
            else
            {
                Gameplay.Play(levelData);
            }
        });
    }
}
