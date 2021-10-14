using System.IO;
using LP;
using NoteSystem.Class;
using UnityEditor;
using UnityEngine;

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

    public LevelDataClass[] levelDataClass;

    private void OnEnable()
    {
        if (info.name == "")
        {
            info.name = name;
        }
    }

    [ContextMenu("Load Musics")]
    private void LoadMusics()
    {
        LP.Module.LoadMusics(this);
    }

    [ContextMenu("Load All")]
    public void LoadAll()
    {
        Module.LoadLevelDatas(this);
    }

    [ContextMenu("Load All LevelPacks")]
    public void LoadAllLevelPacks()
    {
        LevelPack[] levelPacks = LP.Module.GetLevelPacks();
        foreach (LevelPack levelPack in levelPacks)
        {
            Module.LoadLevelDatas(levelPack);
            Module.LoadMusics(levelPack);
        }
    }

    [ContextMenu("Create Scriptable Objects")]
    public void IntializeLevelDataClass()
    {
        foreach (LevelDataClass dataClass in levelDataClass)
        {
            LevelData data = ScriptableObject.CreateInstance<LevelData>();
            data.info = dataClass.info;
            AssetDatabase.Refresh();
            AssetDatabase.CreateAsset(data, $"Assets/Resources/Levels/Datas/{info.name}/{data.info.name}.asset");
            AssetDatabase.SaveAssets();
        }
    }

    [System.Serializable]
    public class LevelDataClass
    {
        public LD.Info info;
    }

}

namespace LP
{
    [System.Serializable]
    public class Info
    {
        public string name;
        public int cost = 10000;
    }

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
                levelData.data.image = LoadSprite(
                    levelPack.info.name,
                    levelData.info.name
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

        public static Sprite LoadSprite(string levelPack, string levelData, string sprite = "default")
        {
            string sector = $"Textures/Levels/";
            string spriteFolder = sector + $"{levelPack}/{levelData}/";
            string spritePath = spriteFolder + spriteFolder;

            Directory.CreateDirectory(Application.dataPath + "/Resources/" + spriteFolder);

            Sprite spriteTexture = Resources.Load<Sprite>(spritePath);

            return spriteTexture;

        }

        public static AudioClip LoadMusic(string levelPack, string levelData, string music)
        {
            string sector = $"Musics/Pack/";
            string musicFolder = sector + $"{levelPack}/{levelData}/";
            string musicPath = musicFolder + music;

            Directory.CreateDirectory(Application.dataPath + "/Resources/" + musicFolder);

            AudioClip audioClip = Resources.Load<AudioClip>(musicPath);

            return audioClip;
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