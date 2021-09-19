using UnityEngine;

[CreateAssetMenu(fileName = "LevelPack", menuName = "Level/LevelPack")]
public class LevelPack : ScriptableObject
{
    public string levelpackName;
    public string difficulty = "F";
    public LevelData[] levelDatas;

    public static LevelPack[] GetLevelPacks(string path = "Levels")
    {
        Debug.Log("Scanning Files on : " + path);
        LevelPack[] levelPacks = Resources.LoadAll<LevelPack>(path);
        Debug.Log(levelPacks.Length + " LevelPacks Found");
        return levelPacks;
    }

    public void LoadAllMusic()
    {
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