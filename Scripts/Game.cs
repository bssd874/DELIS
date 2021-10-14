using System;
using System.IO;
using UnityEngine;
public static class Game
{
    public static T GetReference<T>(this GameObject gameObject, string path)
    {
        T component = gameObject.transform.Find(path).GetComponent<T>();
        if (component == null) Debug.Log($"{path} is not found");
        return component;
    }
    /*
    public static T GetReference<T>(GameObject gameObject, string path)
    {
        return gameObject.transform.Find(path).GetComponent<T>();
    }
    */

    public static AudioClip GetMusic(string name)
    {
        return Resources.Load<AudioClip>($"Musics/{name}");
    }

    public static void Save(object o, string f = "/null.json", string p = "/null")
    {
        string path = Application.persistentDataPath + p;
        string file = path + f;
        Directory.CreateDirectory(path);

        string json = JsonUtility.ToJson(o);
        File.WriteAllText(file, json);
        Debug.Log($"Save Success to {file} with data {json}");
    }

    public static bool Load(object o, string f, string p = "/")
    {
        string path = Application.persistentDataPath + p;
        string file = path + f;
        Directory.CreateDirectory(path);

        bool exist = File.Exists(file);
        if (exist)
        {
            string json = File.ReadAllText(file);
            JsonUtility.FromJsonOverwrite(json, o);
            Debug.Log($"Load Success from {file} to {o.ToString()} with data {json}");
        }
        return exist;
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
            Game.Save(_main, "/UserData.json", "/User");
        }
        public static void Load()
        {
            Game.Load(main, "/UserData.json", "/User");
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