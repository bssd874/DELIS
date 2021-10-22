using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public UserStats userStats;
    public void Result()
    {
        ApplyResult();
        gameObject.SetActive(true);

        userStats.UpdateStats(Gameplay.levelData);
    }

    public void ApplyResult()
    {
        Gameplay.levelData.LoadResult();
        LevelResult levelResult = Gameplay.levelData.levelResult;
        if (GameplayData.highCombo > levelResult.highCombo) levelResult.highCombo = GameplayData.highCombo;
        if (GameplayData.highScore > levelResult.highScore) levelResult.highScore = GameplayData.highScore;
        Gameplay.levelData.SaveResult();

        ApplyReward(GameplayData.highScore);
    }

    public void ApplyReward(float score)
    {
        int reward = (int)((score / 1000000) * 5000);
        Debug.Log(reward);
        Game.player.jPoint += reward;
        Game.player.Save();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back()
    {
        LoadingScreen.Load(() => SceneManager.LoadScene("LevelSelector"));
    }
}
