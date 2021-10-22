using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector main;
    public static LevelPack levelPack;
    public static LevelData levelData;
    public LevelPack _levelPack;

    public static void Enter(LevelPack levelPack)
    {
        LevelSelector.levelPack = levelPack;
        SceneManager.LoadScene("LevelSelector");
    }

    private void Awake()
    {
        main = this;
        if (levelPack is null) levelPack = _levelPack;
    }

    private void Start()
    {
        levelPack.LoadMusics();
        LevelDataSelector.main.CreateLevelDataWindows();

        if (levelPack.levelDatas.Contains(levelData)) Select(levelData);
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

    public void Back()
    {
        LoadingScreen.Load(() => MainMenu.Menu());
    }
}
