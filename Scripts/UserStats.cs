using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    public static UserStats main;

    public TMP_Text RhighCombo;
    public TMP_Text RhighScore;

    public float currentCombo = 0;
    public float currentScore = 0;

    LTDescr LTcombo;
    LTDescr LTscore;


    private void Awake()
    {
        main = this;
    }

    public void UpdateStats(LevelData levelData)
    {
        levelData.LoadResult();
        LevelResult result = levelData.levelResult;

        UpdateProperties(result.highCombo, result.highScore);
    }

    public void UpdateProperties(int combo, float score)
    {
        if (LTcombo != null) LeanTween.cancel(LTcombo.id);
        if (LTscore != null) LeanTween.cancel(LTscore.id);


        LTcombo = LeanTween.value(
            gameObject,
            (float i) =>
            {
                currentCombo = i;
                RhighCombo.text = ((int)currentCombo).ToString();
            },
            currentCombo, combo, 0.5f
        ).setEaseOutExpo().setOnComplete(
            () =>
            {
                currentCombo = combo;
                RhighCombo.text = ((int)currentCombo).ToString();
            }
        );

        LTscore = LeanTween.value(
            gameObject,
            (float i) =>
            {
                currentScore = i;
                RhighScore.text = ((int)currentScore).ToString();
            },
            currentScore, score, 0.5f
        ).setEaseOutExpo().setOnComplete(
            () =>
            {
                currentScore = score;
                RhighScore.text = ((int)currentScore).ToString();
            }
        );
    }

}
