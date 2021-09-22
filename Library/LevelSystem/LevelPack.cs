using UnityEngine;

[CreateAssetMenu(fileName = "LevelPack", menuName = "Level/LevelPack")]
public class LevelPack : ScriptableObject
{
    public string packName;
    public string difficulty = "F";
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