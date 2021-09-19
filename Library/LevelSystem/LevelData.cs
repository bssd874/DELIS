using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
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

    public void OnEnable()
    {
        LoadNoteMaps();
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

    public static void Initialize(LevelData levelData, int index = 0)
    {
        Database.GameplayData.levelResult = levelData.noteMaps[index].levelResult;
        Database.GameplayData.audioClip = levelData.audioClip;
        Database.GameplayData.noteMap = levelData.noteMaps[index];
    }
}