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

        NoteMap[] loadedNoteMaps;

        TextAsset[] noteMapApplicationTextAsset = Resources.LoadAll<TextAsset>("Levels/" + levelName + "/NoteMaps");
        string[] noteMapApplicationJsons = new string[noteMapApplicationTextAsset.Length];
        NoteMap[] noteMapApplicationObjects = new NoteMap[noteMapApplicationJsons.Length];
        
        for (int i = 0; i < noteMapApplicationJsons.Length; i++) 
        {
            string noteMapJson = noteMapApplicationTextAsset[i].text;

            noteMapApplicationJsons[i] = noteMapJson;

            noteMapApplicationObjects[i] = JsonUtility.FromJson<NoteMap>(noteMapJson);
        }

        string externalPath = Application.persistentDataPath + "/Resources/Levels/" + levelName + "/NoteMaps";
        if(!Directory.Exists(externalPath)) Directory.CreateDirectory(externalPath);

        string[] noteMapExternalPaths = Directory.GetFiles(externalPath, "*.json");
        string[] noteMapExternalJsons = new string[noteMapExternalPaths.Length];
        NoteMap[] noteMapExternalObjects = new NoteMap[noteMapExternalPaths.Length];

        for (int i = 0; i < noteMapExternalJsons.Length; i++) 
        {
            string noteMapJson = File.ReadAllText(noteMapExternalPaths[i]);

            noteMapExternalJsons[i] = noteMapJson;

            noteMapExternalObjects[i] = JsonUtility.FromJson<NoteMap>(noteMapJson);
        }

        loadedNoteMaps = noteMapApplicationObjects.Concat<NoteMap>(noteMapExternalObjects).ToArray();

        noteMaps = loadedNoteMaps;

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