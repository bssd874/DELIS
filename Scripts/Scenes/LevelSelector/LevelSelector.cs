using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    // Core

    public static LevelPack levelPack;
    public static LevelData levelData;
    public static int notemapIndex = 0;

    public Transform levelDataContainer;
    public GameObject levelDataWindow;

    // Default

    void Start()
    {
        Initialize();
    }

    // Main

    public void Initialize()
    {
        LevelDataWindow.prefabWindow = levelDataWindow;
        LevelDataWindow.CreateWindows(levelDataContainer, levelPack.levelDatas);
    }

    public static void Select(LevelData selected)
    {
        levelData = selected;
        Play(selected);
    }

    public static void Play(LevelData selected)
    {
        if (User.Data._main.creator) NoteMapCreator.Create(selected);
        LoadingScreen.Load(() => Gameplay.Play(selected));
    }


    // Function

    // GUI Command

}

public class LevelDataWindow
{

    public static LevelDataWindow[] levelDataWindows;

    public static GameObject prefabWindow;
    public GameObject window;
    public LevelData levelData;
    public ReferencePaths rp = new ReferencePaths();
    public ReferenceObjects ro = new ReferenceObjects();

    public LevelDataWindow(Transform Parent, LevelData levelData)
    {
        this.levelData = levelData;
        window = GameObject.Instantiate(prefabWindow, Parent);
        Update();
    }

    public void Update()
    {
        ro.GetReferences(window, rp);

        ro.title.text = levelData.info.name;
        ro.select.onClick.AddListener(Select);

    }

    public class ReferencePaths
    {
        public string title = "Title/Text";
        public string select = "Button";
    }
    public class ReferenceObjects
    {
        public TMP_Text title;
        public Button select;
        public void GetReferences(GameObject o, ReferencePaths p)
        {
            title = Game.GetReference<TMP_Text>(o, p.title);
            select = Game.GetReference<Button>(o, p.select);
        }
    }

    public void Select()
    {
        LevelSelector.Select(levelData);
    }

    public static LevelDataWindow[] CreateWindows(Transform parent, LevelData[] levelDatas)
    {
        LevelDataWindow[] levelDataWindows = new LevelDataWindow[levelDatas.Length];
        for (int i = 0; i < levelDataWindows.Length; i++)
        {
            LevelDataWindow LevelDataWindow = new LevelDataWindow(parent, levelDatas[i]);
            levelDataWindows[i] = LevelDataWindow;
        }

        LevelDataWindow.levelDataWindows = levelDataWindows;

        return levelDataWindows;
    }

}