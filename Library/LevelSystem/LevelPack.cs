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
        // Levels/NoteMap/LevelPack/LevelData/Music
        public static void LoadLevelDatas(LevelPack levelPack)
        {
            levelPack.levelDatas = LD.Module.GetLevelDatas(levelPack.info.name);
            LD.Module.LoadLevelDatas(levelPack.info.name, levelPack.levelDatas);
        }
        public static LevelPack[] GetLevelPacks()
        {
            string pathFolder = $"Levels/Packs/";

            LevelPack[] levelPacks = Resources.LoadAll<LevelPack>(pathFolder);

            return levelPacks;
        }

        public static void LoadLevelPacks(LevelPack[] levelPacks)
        {
            for (int i = 0; i < levelPacks.Length; i++) 
            {
                LoadLevelPack(levelPacks[i]);
            }
        }

        static void LoadLevelPack(LevelPack levelPack)
        {
            LP.Module.LoadData(levelPack);
            levelPack.LoadAll();
        }

        // Format Musik Adalah "LevelPack/LevelData/Musik"
        public static void LoadMusics(LevelPack levelPack)
        {
            for (int i = 0; i < levelPack.levelDatas.Length; i++)
            {
                LevelData levelData = levelPack.levelDatas[i];
                levelData.data.audioClip = LoadMusic(
                    levelPack.info.name,
                    levelData.info.name,
                    levelData.info.music
                    );
            }
        }
        public static void UnLoadMusics(LevelPack levelPack)
        {
            for (int i = 0; i < levelPack.levelDatas.Length; i++)
            {
                Resources.UnloadAsset(levelPack.levelDatas[i].data.audioClip);
            }
        }

        static AudioClip LoadMusic(string levelPack, string levelData, string music)
        {
            string sector = $"Musics/Pack/";
            string musicFolder = sector + $"{levelPack}/{levelData}/";
            string musicPath = musicFolder + $"{music}.mp3";

            Directory.CreateDirectory(musicFolder);

            return Resources.Load<AudioClip>(musicPath);
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