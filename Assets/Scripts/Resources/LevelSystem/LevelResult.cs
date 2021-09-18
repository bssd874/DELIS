using System.IO;
using UnityEngine;

[System.Serializable]
public class LevelResult
{
    public string name;
    public int maxCombo;
    public int maxScore;

    public void SaveResult(string levelName)
    {
        string path = Application.persistentDataPath + "/SaveData/Levels";

        string filePath = Application.persistentDataPath + "/SaveData/Levels/" + levelName + ".levelresult";

        Debug.Log("Saved to : " + filePath);

        string json = JsonUtility.ToJson(this);

        Directory.CreateDirectory(path);

        name = levelName;

        File.WriteAllText(filePath, json);
    }

    public void LoadResult()
    {
        LevelResult levelResult = new LevelResult();

        string path = Application.persistentDataPath + "/SaveData/Levels";

        string filePath = Application.persistentDataPath + "/SaveData/Levels/" + name + ".levelresult";

        Directory.CreateDirectory(path);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    public static LevelResult GetResult(string levelName)
    {
        LevelResult levelResult = new LevelResult();

        string path = Application.persistentDataPath + "/SaveData/Levels";

        string filePath = Application.persistentDataPath + "/SaveData/Levels/" + levelName + ".levelresult";

        Directory.CreateDirectory(path);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            levelResult = JsonUtility.FromJson<LevelResult>(json);
        }

        return levelResult;
    }
}