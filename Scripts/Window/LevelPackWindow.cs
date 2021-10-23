using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPackWindow : MonoBehaviour
{
    public LevelPack levelPack;

    public TMP_Text title;
    public TMP_Text cost;
    public Button buyButton;

    public LevelDataDisplayer levelDataDisplayer;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        levelPack.LoadData();

        title.text = levelPack.name;
        cost.text = levelPack.cost.ToString();
        buyButton.onClick.AddListener(Buy);

        levelDataDisplayer.levelPack = levelPack;
        levelDataDisplayer.CreateLevelDataPanels();

        buyButton.gameObject.SetActive(!levelPack.data.purchased);
    }

    public void Buy()
    {
        levelPack.Buy();
        Initialize();
        JPanel.main.UpdatePanel();
    }

    public void Enter()
    {
        MainMenu.main.Enter(levelPack);
    }

}
