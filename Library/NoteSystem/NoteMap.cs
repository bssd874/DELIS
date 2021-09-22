using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteMap
{
    public string code;
    public LevelResult levelResult;
    public LevelResult _levelResult
    {
        set
        {
            levelResult = value;
        }
        get
        {
            if (levelResult == null) return new LevelResult();
            return levelResult;
            
        }
    }
    public NotePack[] notePacks;

    public int totalNotes
    {
        get
        {
            int total = 0;
            foreach (NotePack notePack in notePacks)
            {
                if (notePack.comboEnabled)
                {
                    total += notePack.noteDatas.Length;
                }
            }
            return total;
        }
    }

    [System.NonSerialized] public List<NoteInstance> noteInstances = new List<NoteInstance>();
    public NoteInstance[] GetNoteInstances()
    {
        
        foreach (NotePack notePack in notePacks)
        {
            noteInstances.AddRange(notePack.GetNoteInstances());
        }

        noteInstances.Sort();

        return noteInstances.ToArray();
    }

    public void Save(string name)
    {
        if (code == "") code = Random.value.ToString();
        string filePath = GetFilePath(name, code);
        File.WriteAllText(
            filePath, 
            JsonUtility.ToJson(_levelResult)
        );
        Debug.Log("Level Result saved to : " + filePath);
    }

    public static LevelResult Load(string name, string code)
    {
        string filePath = GetFilePath(name, code);
        if (!File.Exists(filePath)) return new LevelResult();
        Debug.Log("Level Result loaded from : " + filePath);
        return JsonUtility.FromJson<LevelResult>(
            File.ReadAllText(filePath)
        );
    }

    public static string GetFilePath (string name, string code)
    {
        string path = Application.persistentDataPath + "/SaveData/Levels/" + name + "/";
        Directory.CreateDirectory(path);
        string filePath = path + code + ".json";
        return filePath;
    }
}