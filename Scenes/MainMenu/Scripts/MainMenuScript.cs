using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript main;

    void Awake()
    {
        main = this;
    }

    public void SelectLevelPack(LevelPack levelPack)
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
