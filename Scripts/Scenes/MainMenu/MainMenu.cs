using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //  Core
    public static LevelPack[] levelPacks;
    public static LevelPack levelPack;

    public Transform levelPackContainer;
    public GameObject levelPackWindow;

    public static GameObject _screen;
    public GameObject screen;
    public GameObject Loading;


    // Default
    void Start()
    {
        _screen = screen;
        LoadingScreen.prefabScreen = Loading;
        User.Data.main.Save();
        User.Data.main.Load();
        Initialize();
    }

    // Main 
    public void Initialize()
    {
        levelPacks = LP.Module.GetLevelPacks();
        LevelPackWindow.prefabWindow = levelPackWindow;
        LevelPackWindow.CreateWindows(levelPackContainer, levelPacks);
    }

    public static void Select(LevelPack selected)
    {
        levelPack = selected;
        LoadingScreen.Load(() => Enter(selected));
    }


    public static void Enter(LevelPack levelPack)
    {
        Debug.Log("Selected");
        LP.Module.LoadMusics(levelPack);
        LevelSelector.levelPack = levelPack;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }

    // Functions

    // GUI Command

}


public class LevelPackWindow
{

    public static LevelPackWindow[] levelPackWindows;

    public static GameObject prefabWindow;
    public GameObject window;
    public LevelPack levelPack;
    public ReferenceObjects r;

    public LevelPackWindow(Transform parent, LevelPack levelPack)
    {
        this.levelPack = levelPack;
        window = GameObject.Instantiate(prefabWindow, parent);
        r = new ReferenceObjects(window);
        UpdateData();
    }

    public void UpdateData()
    {

        r.title.text = levelPack.info.name;
        r.enter.onClick.AddListener(Enter);
        r.buy.onClick.AddListener(Buy);

        CheckPurchased();
    }

    public class ReferencePaths
    {
        public static string title = "NAME/Text";
        public static string enter = "ENTER";
        public static string buy = "BUY";
    }
    public class ReferenceObjects
    {
        public TMP_Text title;
        public Button enter;
        public Button buy;

        public ReferenceObjects(GameObject o)
        {
            title = Game.GetReference<TMP_Text>(o, ReferencePaths.title);
            enter = Game.GetReference<Button>(o, ReferencePaths.enter);
            buy = Game.GetReference<Button>(o, ReferencePaths.buy);
        }
    }

    public void Enter()
    {
        MainMenu.Select(levelPack);
    }

    public void Buy()
    {
        User.Data.main.Load();
        LP.Module.LoadData(levelPack);
        if (levelPack._data.purchased) return;
        if (User.Data.main.JPoints >= levelPack.info.cost)
        {
            User.Data.main.JPoints -= levelPack.info.cost;
            User.Data.main.Save();
            levelPack._data.purchased = true;
            LP.Module.SaveData(levelPack);
            CheckPurchased();
        }

    }

    public void CheckPurchased()
    {
        LP.Module.LoadData(levelPack);
        if (levelPack._data.purchased)
        {
            r.buy.gameObject.SetActive(false);
        }
        else
        {
            r.buy.gameObject.SetActive(true);
        }
    }

    public static LevelPackWindow[] CreateWindows(Transform parent, LevelPack[] levelPacks)
    {
        LevelPackWindow[] levelPackWindows = new LevelPackWindow[levelPacks.Length];
        for (int i = 0; i < levelPackWindows.Length; i++)
        {
            LevelPackWindow levelPackWindow = new LevelPackWindow(parent, levelPacks[i]);
            levelPackWindows[i] = levelPackWindow;
        }

        LevelPackWindow.levelPackWindows = levelPackWindows;

        return levelPackWindows;
    }

}