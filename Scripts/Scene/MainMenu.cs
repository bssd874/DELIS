using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu main;
    public GameObject loadingScreen;
    public static LevelPack[] levelPacks;

    private void Awake()
    {
        main = this;
    }

    public static void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        levelPacks = LevelPackExtensions.LoadLevelPacks();

        LoadingScreen.ScreenPrefab = loadingScreen;

        Player.Load(Game.player);
        JPanel.main.UpdatePanel();
        //PlayerPanel.main.UpdatePanel();

        LevelPackSelector.main.CreateLevelPackWindows();
    }

    public void Enter(LevelPack levelPack)
    {
        LoadingScreen.Load(() => LevelSelector.Enter(levelPack));
    }

}
