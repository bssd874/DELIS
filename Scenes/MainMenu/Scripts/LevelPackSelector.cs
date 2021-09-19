using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPackSelector : MonoBehaviour
{

    public RectTransform PackGrid;
    public GameObject LevelPackObject;

    public void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        LevelPack[] levelPacks = Database.GameData.levelpacks;
        foreach (LevelPack levelPack in levelPacks)
        {
            GameObject packObject = Instantiate(LevelPackObject, PackGrid);
            LevelPackWindow packWindow = packObject.GetComponent<LevelPackWindow>();
            packWindow.levelPack = levelPack;
            packWindow.Initialize();
        }
    }
}
