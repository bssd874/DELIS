using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    public AudioClip audioClip;
    public LevelResult levelResult;
    public LevelProperties levelProperties;
    public NoteMap[] noteMaps;
    public Sprite[] sprites;

    public void OnEnable()
    {
        Debug.Log("Awaken");
        LoadNoteMaps();
    }

    public void LoadNoteMaps()
    {
        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Levels/PERSONAL COLLECTION/" + levelProperties.levelName);

        noteMaps = new NoteMap[textAssets.Length];

        for (int i = 0; i < textAssets.Length; i++)
        {

            string json = textAssets[i].text;

            noteMaps[i] = JsonUtility.FromJson<NoteMap>(json);
        }
    }

    public static void Initialize(LevelData levelData, int index = 0)
    {
        GameplayData.levelResult = levelData.levelResult;
        GameplayData.audioClip = levelData.audioClip;
        GameplayData.noteMap = levelData.noteMaps[index];
    }
}