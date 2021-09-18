using UnityEngine;

[CreateAssetMenu(fileName = "LevelPack", menuName = "Level/LevelPack")]
public class LevelPack : ScriptableObject
{
    public string levelpackName;
    public LevelData[] levelData;

    public static LevelPack[] GetLevelPacks(string path = "Levels")
    {
        Debug.Log("Scanning Files on : " + path);
        LevelPack[] levelPacks = Resources.LoadAll<LevelPack>(path);
        Debug.Log(levelPacks.Length + " LevelPacks Found");
        return levelPacks;
    }
}