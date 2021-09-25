using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript main;
    public static LevelPack levelPack;

    void Awake()
    {
        main = this;
    }

    public void SelectLevelPack(LevelPack selectedlevelPack)
    {
        levelPack = selectedlevelPack;
        LoadingScreenManager.main.LoadingScreen(1, Select);
    }

    public void Select()
    {
        Database.GameData.levelPack = levelPack;
        levelPack.LoadAllMusic();
        SceneManager.LoadScene("LevelSelector");
    }

    public void Back()
    {
        Application.Quit();
    }

}
