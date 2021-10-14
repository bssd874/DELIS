using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
public static class Game
{
    public static void SetReference(this GameObject gameObject, object target, string path)
    {
        object obj = gameObject.transform.Find(path).GetComponent(target.GetType());
        if (obj == null) Debug.Log($"{path} is not found");
        target = obj;
    }

    public static AudioClip GetMusic(string name)
    {
        return Resources.Load<AudioClip>($"Musics/{name}");
    }

    public static void Save(this object obj, string filePath, bool createDirectory = false)
    {
        string folderPath = Path.GetDirectoryName(filePath);
        if (createDirectory) Directory.CreateDirectory(folderPath); Debug.Log($"Directory created : {folderPath}");

        string data = JsonUtility.ToJson(obj);
        File.WriteAllText(filePath, data); Debug.Log($"Data writed to {filePath} with data {data}");
    }

    public static void Load(this object obj, string filePath, bool createDirectory = false)
    {
        string folderPath = Path.GetDirectoryName(filePath);
        if (createDirectory) Directory.CreateDirectory(folderPath); Debug.Log($"Directory created : {folderPath}");

        if (!Directory.Exists(filePath)) return;

        string data = File.ReadAllText(filePath);

        JsonUtility.FromJsonOverwrite(data, obj);
    }
}

public class User
{


    public class Data
    {
        public static Data main = new Data();
        public static Data _main
        {
            set
            {
                main = value;
                Save();
            }
            get
            {
                Load();
                return main;
            }
        }


        public bool creator = false;
        public bool autoplay = false;
        public string name = "";
        public bool firstTimePlaying = true;
        public int JPoints = 10000;

        public static void Save()
        {
            if (main == null) main = new Data();
            Game.Save(_main, "/User/UserData.json");
        }
        public static void Load()
        {
            Game.Load(main, "/User/UserData.json");
        }
    }


}

public class ScoreSystem
{
    public static int totalNotes = 100;
    public static float P { get { return 900000f / totalNotes; } }
    public static float C { get { return 100000f / (totalNotes * (totalNotes - 1) / 2); } }
    public static float B { get { return (GameplayData.Stats.combo - 1) * C; } }
    public static float GetPerfect()
    {
        return P + B;
    }
    public static float GetGood()
    {
        return 0.7f * P + B;
    }
    public static float GetBad()
    {
        return 0.3f * P;
    }
    public static float GetMiss()
    {
        return 0;
    }
}

public class ComboSystem
{
    public static void ValidateCombo(NoteState noteState)
    {
        float score = 0;

        switch (noteState)
        {
            case NoteState.Perfect:
                GameplayData.Stats.combo += 1;
                GameplayData.Stats.ComboInfo.Perfect += 1;
                score = ScoreSystem.GetPerfect();
                break;
            case NoteState.Good:
                GameplayData.Stats.combo += 1;
                GameplayData.Stats.ComboInfo.Good += 1;
                score = ScoreSystem.GetGood();
                break;
            case NoteState.Bad:
                GameplayData.Stats.combo = 0;
                GameplayData.Stats.ComboInfo.Bad += 1;
                score = ScoreSystem.GetBad();
                break;
            case NoteState.Miss:
                GameplayData.Stats.combo = 0;
                GameplayData.Stats.ComboInfo.Miss += 1;
                score = ScoreSystem.GetMiss();
                break;
        }
        GameplayData.Stats.score += score;
        GameplayData.Stats.comboUpdate.Invoke();

        GameplayData.Stats.scoreUpdate.Invoke();
    }
}