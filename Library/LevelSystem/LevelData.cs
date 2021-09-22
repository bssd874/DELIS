using UnityEngine;

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
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Levels/" + levelName + "/NoteMaps/");

        Debug.Log("Levels/" + levelName + "/NoteMaps/");

        noteMaps = new NoteMap[textAssets.Length];

        for (int i = 0; i < textAssets.Length; i++)
        {

            string json = textAssets[i].text;

            noteMaps[i] = JsonUtility.FromJson<NoteMap>(json);
        }
    }

    public void LoadAllData()
    {
        foreach(NoteMap noteMap in noteMaps)
        {
            noteMap.levelResult = NoteMap.Load(levelName, noteMap.code);
        }
    }
    public void SaveAllData()
    {
        foreach(NoteMap noteMap in noteMaps)
        {
            noteMap.Save(levelName);
        }
    }

    public NoteMap GetNoteMap(int index)
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