using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [Header("Tutorial")]
    public LevelPack levelPack;
    public LevelData levelData;
    [Header("Assets")]
    public GameObject loadingScreenPrefab;
    private void Start()
    {
        Player.Load(Game.player);
        InitializeAssets();
    }
    void InitializeAssets()
    {
        LoadingScreen.ScreenPrefab = loadingScreenPrefab;
    }
    public void Play()
    {
        if (Game.player.tutorial) playTutorial();
        else MainMenu.Menu();
    }

    public void playTutorial()
    {
        MainMenu.levelPacks = LevelPackExtensions.LoadLevelPacks();
        LevelSelector.levelPack = levelPack;
        LevelSelector.levelPack.LoadAllLevelData();
        LoadingScreen.Load(() => Gameplay.Play(levelData));
        Game.player.tutorial = false;
        Game.player.Save();
    }
}
