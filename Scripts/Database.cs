using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Database
{
    public static class GameplayData
    {
        public static LevelData levelData;
        public static LevelResult levelResult = null;
        public static AudioClip audioClip
        {
            get
            {
                return levelData.audioClip;
            }
        }
        public static NoteMap noteMap = null;
    }

    public static class GameData
    {
        public static LevelPack[] levelpacks
        {
            get
            {
                LevelPack[] packs = Resources.LoadAll<LevelPack>("LevelPacks");
                return packs;
            }
        }

        public static LevelPack levelPack = null;

        public static LevelData[] levelDatas
        {
            get
            {
                return levelPack.levelDatas;
            }
        }
    }

    public static class User
    {
        
        public static Data data;
        public static Data _data
        {
            set
            {
                data = value;
            }
            get
            {
                if (data == null) data = Data.Load();
                if (data == null) data = new Data();

                Data.Save(data);

                return data;
            }
        }
        

        public class Data
        {
            public int JPoints = 1000000;

            public static void Save(Data data)
            {
                string filePath = GetFilePath();
                File.WriteAllText(
                    filePath, 
                    JsonUtility.ToJson(data)
                );
                Debug.Log("User Data saved to : " + filePath);
            }

            public static Data Load()
            {
                string filePath = GetFilePath();
                if (!File.Exists(filePath)) return new Data();
                Debug.Log("User Data loaded from : " + filePath);
                return JsonUtility.FromJson<Data>(
                    File.ReadAllText(filePath)
                );
            }

            public static string GetFilePath ()
            {
                string path = Application.persistentDataPath + "/SaveData/User/";
                Directory.CreateDirectory(path);
                string filePath = path + "Data" + ".json";
                return filePath;
            }
        }

    }
}