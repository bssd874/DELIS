using UnityEngine;
using System.Linq;
using System.IO;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "";
    public AudioClip music;
    public AudioClip audioClip
    {
        get
        {
            if (!music)
            {
                music = Resources.Load<AudioClip>("Audio/" + levelName);
            }
            return music;
        }
    }
    public NoteMap[] noteMaps;
    public Sprite[] sprites;

    public float skills
    {
        get
        {
            float value = 0;
            int total = 0;
            foreach(NoteMap noteMap in noteMaps)
            {
                value += NoteMap.Load(levelName, noteMap.code).skill;
                total += 1;
            }
            return value / total;
        }
    }

    public void OnEnable()
    {
        LoadNoteMaps();
        LoadAllData();
    }

    public void LoadNoteMaps()
    {
        string[] noteMapPaths;
        string[] noteMapJsons;
        NoteMap[] noteMapsObjects;

        

        string applicationPath = Application.dataPath + "/Resources/Levels/" + levelName + "/NoteMaps";
        string globalPath = Application.persistentDataPath + "/Resources/Levels/" + levelName + "/NoteMaps";

        if(!Directory.Exists(applicationPath)) Directory.CreateDirectory(applicationPath);
        if(!Directory.Exists(globalPath)) Directory.CreateDirectory(globalPath);
        
        string[] noteMapApplicationPath = Directory.GetFiles(applicationPath, "*.json");
        string[] noteMapGlobalPath = Directory.GetFiles(globalPath, "*.json");        

        noteMapPaths = noteMapApplicationPath.Union<string>(noteMapGlobalPath).ToArray();

        noteMapJsons = new string[noteMapPaths.Length];
        noteMapsObjects = new NoteMap[noteMapJsons.Length];

        Debug.Log(noteMapGlobalPath.Length);

        for (int i = 0; i < noteMapPaths.Length; i++) 
        {
            Debug.Log(noteMapPaths[i]);

            string noteMapJson = File.ReadAllText(noteMapPaths[i]);

            noteMapsObjects[i] = JsonUtility.FromJson<NoteMap>(noteMapJson);

            noteMapJsons[i] = noteMapJson;
        }

        noteMaps = noteMapsObjects;
    }

    public void LoadAllData()
    {
        foreach(NoteMap noteMap in noteMaps)
        {
            noteMap.levelResult = NoteMap.Load(levelName, noteMap.code);
        }
    }

    public void SaveNoteMap(NoteMap noteMap)
    {
        noteMap.Save(levelName);
    }

    public void SaveAllData()
    {
        foreach(NoteMap noteMap in noteMaps)
        {
            noteMap.Save(levelName);
        }
    }

    public NoteMap GetNoteMapIndex(int index)
    {
        return noteMaps[Mathf.Clamp(index, 0, noteMaps.Length)];
    }

    public static void Initialize(LevelData levelData, int index = 0)
    {
        Database.GameplayData.levelData = levelData;
        Database.GameplayData.levelResult = levelData.noteMaps[index].levelResult;
        Database.GameplayData.noteMap = levelData.noteMaps[index];
    }
}