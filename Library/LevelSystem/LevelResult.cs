using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelResult
{
    public int highCombo = 0;
    public int highScore = 0;
    public float skill = 0;
    public int timesPlayed = 0;
    public string Rank = "F";

    public void SaveResult(string levelName, int index)
    {
        string path = Application.persistentDataPath + "/SaveData/Levels";

        string filePath = getPath(levelName, index);

        Debug.Log("Saved to : " + filePath);

        string json = JsonUtility.ToJson(this);

        Directory.CreateDirectory(path);

        File.WriteAllText(filePath, json);
    }

    public void LoadResult(string levelName, int index)
    {
        LevelResult levelResult = new LevelResult();

        string path = Application.persistentDataPath + "/SaveData/Levels";

        string filePath = getPath(levelName, index);

        Directory.CreateDirectory(path);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            JsonUtility.FromJsonOverwrite(json, this);
        }
        
    }

    public static LevelResult GetResult(string levelName, int index)
    {
        LevelResult levelResult = new LevelResult();

        string path = Application.persistentDataPath + "/SaveData/Levels";

        string filePath = getPath(levelName, index);

        Directory.CreateDirectory(path);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            levelResult = JsonUtility.FromJson<LevelResult>(json);
        }

        return levelResult;
    }

    public static string getPath(string levelName, int index)
    {
        return Application.persistentDataPath + "/SaveData/Levels/" + levelName + "/" + index.ToString() + ".json";
    }

}