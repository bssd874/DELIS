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
        levelPacks = LevelPackExtensions.LoadLevelPacks();
    }

    public static void Menu()
    {
        LoadingScreen.Load(() => SceneManager.LoadScene("MainMenu"));
    }

    private void Start()
    {

        LoadingScreen.ScreenPrefab = loadingScreen;

        Player.Load(Game.player);
        JPanel.main.UpdatePanel();
        PlayerPanel.main.UpdatePanel();

        LevelPackSelector.main.CreateLevelPackWindows();
    }

    public void Enter(LevelPack levelPack)
    {
        LoadingScreen.Load(() => LevelSelector.Enter(levelPack));
    }

}
