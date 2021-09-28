using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPack", menuName = "Level/LevelPack")]
public class LevelPack : ScriptableObject
{
    public string packName;

    public LevelPackInfo levelPackInfo;
    public LevelPackData levelPackData;
    public LevelPackData _LevelPackData
    {
        set
        {
            levelPackData = value;
        }
        get
        {
            if (levelPackData == null) levelPackData = LevelPackData.Load(this);
            if (levelPackData == null) levelPackData = new LevelPackData();

            return levelPackData;
        }
    }

    public void SaveData()
    {
        LevelPackData.Save(this, _LevelPackData);
    }

    public LevelData[] levelDatas = new LevelData[0];

    public static LevelPack[] GetLevelPacks(string path = "Levels")
    {
        LevelPack[] levelPacks = Resources.LoadAll<LevelPack>(path);
        return levelPacks;
    }

    public float skills
    {
        get
        {
            float value = 0;
            int total = 0;
            foreach(LevelData levelData in levelDatas)
            {
                value += levelData.skills;
                total += 1;
            }
            return value / total;
        }
    }

    public void LoadAllMusic()
    {

        _LevelPackData.unlocked = true;
        SaveData();

        foreach (LevelData levelData in levelDatas)
        {
            AudioClip clip = levelData.audioClip;
        }
    }

    public void UnloadAllMusic()
    {
        foreach (LevelData levelData in levelDatas)
        {
            Resources.UnloadAsset(levelData.music);
            levelData.music = null;
        }
    }
}

[System.Serializable]
public class LevelPackInfo
{
    public int cost;
}

public class LevelPackData
{
    public bool unlocked;

    public static void Save(LevelPack levelPack, LevelPackData levelPackData)
    {
        string filePath = GetFilePath(levelPack);
        File.WriteAllText(
            filePath, 
            JsonUtility.ToJson(levelPackData)
        );
        Debug.Log("Level Pack saved to : " + filePath);
    }

    public static LevelPackData Load(LevelPack levelPack)
    {
        string filePath = GetFilePath(levelPack);
        if (!File.Exists(filePath)) return new LevelPackData();
        Debug.Log("Level Pack loaded from : " + filePath);
        return JsonUtility.FromJson<LevelPackData>(
            File.ReadAllText(filePath)
        );
    }

    public static string GetFilePath (LevelPack levelPack)
    {
        string path = Application.persistentDataPath + "/SaveData/LevelPacks/";
        Directory.CreateDirectory(path);
        string filePath = path + levelPack.packName + ".json";
        return filePath;
    }
}