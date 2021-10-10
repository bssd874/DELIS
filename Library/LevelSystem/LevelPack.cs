using NoteSystem.Class;
using UnityEngine;
using LP;
using System.IO;

[CreateAssetMenu(fileName = "LevelPack", menuName = "LevelSystem/LevelPack")]
//meyimpan level data
public class LevelPack : ScriptableObject
{
    public Info info;
    public Data data;
    public Data _data
    {
        set
        {
            data = value;
        }
        get
        {
            if (data == null) data = new Data();
            return data;
        }
    }
    public LevelData[] levelDatas;

    [ContextMenu("Load All")]
    public void LoadAll()
    {
        Module.LoadLevelDatas(this);
    }
}

namespace LP
{
    [System.Serializable]
    public class Info
    {
        public string name;
        public int cost = 0;
    }

    [System.Serializable]
    public class Data
    {            
        public bool purchased = false;
    }

    public class Module
    {
        public static void LoadLevelDatas(LevelPack levelPack)
        {
            string pathPack = $"Levels/NoteMap/{levelPack.info.name}";
            levelPack.levelDatas = Resources.LoadAll<LevelData>($"Levels/LevelData/{levelPack.info.name}/");
            for (int i = 0; i < levelPack.levelDatas.Length; i++) 
            {
                string pathData = $"{pathPack}/{levelPack.levelDatas[i].info.name}/";

                TextAsset[] textAssets = Resources.LoadAll<TextAsset>(pathData);
                NoteMap[] noteMaps = new NoteMap[textAssets.Length];
                for (int j = 0; j < noteMaps.Length; j++)
                {
                    noteMaps[j] = JsonUtility.FromJson<NoteMap>(textAssets[j].text);
                }
                levelPack.levelDatas[i].noteMaps = noteMaps;
            }
        }
        public static LevelPack[] GetLevelPacks()
        {
            string path = $"Levels/LevelPack/";
            LevelPack[] levelPacks = Resources.LoadAll<LevelPack>(path);
            for (int i = 0; i < levelPacks.Length; i++) 
            {
                LP.Module.LoadData(levelPacks[i]);
                LP.Module.LoadLevelDatas(levelPacks[i]);
            }
            return levelPacks;
        }

        public static void LoadMusic(LevelData[] levelDatas)
        {
            for (int i = 0; i < levelDatas.Length; i++)
            {
                string pathMusic = $"Musics/{levelDatas[i].info.music}";
                levelDatas[i].data.audioClip = Resources.Load<AudioClip>(pathMusic);
            }
        }
        public static void UnLoadMusic(LevelData[] levelDatas)
        {
            for (int i = 0; i < levelDatas.Length; i++)
            {
                Resources.UnloadAsset(levelDatas[i].data.audioClip);
            }
        }

        public static void SaveData(LevelPack levelPack)
        {
            Game.Save(levelPack._data, $"/{levelPack.info.name}.json", "/User/LevelPack");
        }

        public static void LoadData(LevelPack levelPack)
        {
            if (!Game.Load(levelPack._data, $"/{levelPack.info.name}.json", "/User/LevelPack"))
            {
                SaveData(levelPack);
                Game.Load(levelPack._data, $"/{levelPack.info.name}.json", "/User/LevelPack");
            }
        }

    }
}