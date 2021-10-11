using NoteSystem.Class;
using UnityEngine;
using System.IO;
using LD;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelSystem/LevelData")]
//menyimpan data
public class LevelData : ScriptableObject
{
    public Info info;
    public Data data;
    public NoteMap[] noteMaps;

    
}

namespace LD
{
    [System.Serializable]
    public class Info // data yg di simpan
    {
        public string name;
        public string music;
        public string description;
        public string source;
    }

    public class Data
    {
        public AudioClip audioClip;
    }

    public class NoteData
    {
        public static void Save(NoteMap noteMap)
        {
            CheckID(noteMap);
            string json = JsonUtility.ToJson(noteMap.data.result);
            string path = Application.persistentDataPath + $"/User/SaveData/" + noteMap.data.ID + ".json";
            File.WriteAllText(path, json);
        }
        public static void Load(NoteMap noteMap)
        {
            CheckID(noteMap);
            string path = Application.persistentDataPath + $"/User/SaveData/" + noteMap.data.ID + ".json";
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, noteMap.data.result);
        }
        public static void CheckID(NoteMap noteMap)
        {
            if (noteMap.data.ID == "")
            {
                noteMap.data.ID = Random.state.ToString();
            }
        }
    }
    

    public class Module
    {
        // Levels/NoteMap/LevelPack/Data/Music

        public static LevelData[] GetLevelDatas(string levelPack)
        {
            string sector = $"Levels/Datas/";
            string levelFolder = sector + $"{levelPack}/";

            Directory.CreateDirectory(Application.dataPath + "/Resources/" + levelFolder);

            return Resources.LoadAll<LevelData>(levelFolder);
        }

        public static void LoadLevelDatas(string levelPack, LevelData[] levelDatas)
        {
            foreach (LevelData levelData in levelDatas)
            {
                LoadLevelData(levelPack, levelData);
            }
        }

        static void LoadLevelData(string levelPack, LevelData levelData)
        {
            levelData.noteMaps = NP.Module.GetNoteMaps(levelPack, levelData.info.name, levelData.info.music);
        }

        
    }

    

}