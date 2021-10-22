using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDataWindow : MonoBehaviour
{
    public LevelData levelData;

    public TMP_Text title;
    public Image image;
    public Button selectButton;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        levelData.LoadData();

        title.text = levelData.levelName;
        //image.sprite = levelData.levelSprite;

        selectButton.onClick.AddListener(Select);
    }

    public void Select()
    {
        LevelSelector.main.Select(levelData);
    }

}
