using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MainMenu : MonoBehaviour
{
    //  Core
    public static LevelPack[] levelPacks;
    public static LevelPack levelPack;

    public Transform levelPackContainer;
    public GameObject levelPackWindow;


    // Default
    void Start()
    {
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
        Enter(selected);
    }


    public static void Enter(LevelPack levelPack)
    {
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
    public ReferencePaths rp = new ReferencePaths();
    public ReferenceObjects ro = new ReferenceObjects();

    public LevelPackWindow(Transform Parent, LevelPack levelPack)
    {
        this.levelPack = levelPack;
        window = GameObject.Instantiate(prefabWindow, Parent);
        Update();
    }

    public void Update()
    {
        ro.GetReferences(window, rp);

        ro.title.text = levelPack.info.name;
        ro.enter.onClick.AddListener(Enter);
        ro.buy.onClick.AddListener(Buy);
        
        CheckPurchased();
    }

    public class ReferencePaths
    {
        public string title = "NAME/Text";
        public string enter = "ENTER";
        public string buy = "BUY";
    }
    public class ReferenceObjects
    {
        public TMP_Text title;
        public Button enter;
        public Button buy;

        public void GetReferences(GameObject o, ReferencePaths p)
        {
            title = Game.GetReference<TMP_Text>(o, p.title);
            enter = Game.GetReference<Button>(o, p.enter);
            buy = Game.GetReference<Button>(o, p.buy);
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
            ro.buy.gameObject.SetActive(false);
        }
        else
        {
            ro.buy.gameObject.SetActive(true);
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