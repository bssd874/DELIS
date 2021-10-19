using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    public static UserStats main;

    public TMP_Text RhighCombo;
    public TMP_Text RhighScore;


    private void Awake()
    {
        main = this;
    }

    public void UpdateStats(LevelData levelData)
    {
        levelData.LoadResult();
        LevelResult result = levelData.levelResult;

        RhighCombo.text = result.highCombo.ToString();
        RhighScore.text = result.highScore.ToString();

    }
}
