using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteCreator : MonoBehaviour
{
    public int difficultyIndex = 0;
    public LevelData levelData;

    public NoteMap noteMap;
    public int index = 0;
    public AudioSource audioSource;
    public LineRenderer lineRenderer;
    public List<NoteData> noteDatas;

    public RectTransform StartIndicator;

    public Transform indicator;
    public float countdown;

    public NotePlayer notePlayer;



    string di = "";
    string i = "";

    string size = "";
    string code = "";

    private void OnGUI()
    {
        if (Screen.orientation != ScreenOrientation.Portrait) return;        

        if (GUILayout.Button("Reset All"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("NoteCreator");
        }
        if (GUILayout.Button("Reset NoteDatas"))
        {
            noteDatas = new List<NoteData>();
        }
        if (GUILayout.Button("ResetAudio"))
        {
            audioSource.Play();
        }
        
        GUILayout.Label("Difficulty Index");
        di = GUILayout.TextField(di);
        GUILayout.Label("Notemap Index");
        i = GUILayout.TextField(i);
        GUILayout.Label("NotePack");
        size = GUILayout.TextField(size);
        GUILayout.Label("Save Code");
        code = GUILayout.TextField(code);
        
        if (GUILayout.Button("Update Notemap"))
        {
            noteMap.notePacks = new NotePack[int.Parse(size)];
            noteMap._code = code;
        }

        if (GUILayout.Button("Update Info"))
        {
            
            difficultyIndex = int.Parse(di);
            
            index = int.Parse(i);
        }

        if (GUILayout.Button("Insert Layer"))
        {
            InsertNoteDataIntoLayer();
        }
        if (GUILayout.Button("Convert Notemap Data"))
        {
            SaveNoteMapData();
        }
        if (GUILayout.Button("Back"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
        }
    }

    private void Start()
    {

        if (!levelData) levelData = LevelSelectorScript._levelData;

        audioSource.clip = levelData.audioClip;

        StartIndicator.LeanScaleY(0, countdown).setOnComplete(()=> { audioSource.Play();});
    }

    private void Update()
    {
        if (Screen.orientation != ScreenOrientation.Landscape) return;
        if (Input.GetKeyDown(KeyCode.A))
        {
            RegisterNotes(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RegisterNotes(Camera.main.ScreenToViewportPoint(touch.position));
            }
        }

    }

    public void RegisterNotes(Vector2 viewportPosition)
    {
        indicator.position = Camera.main.ViewportToWorldPoint(viewportPosition) + new Vector3(0, 0, 10);

        Camera.main.backgroundColor = Random.ColorHSV();

        NoteData noteData = new NoteData();
        noteData.position = viewportPosition;
        noteData.time = audioSource.time;

        noteDatas.Add(noteData);
    }

    public void InsertNoteDataIntoLayer()
    {
        List<NotePack> packs = new List<NotePack>(noteMap.notePacks);
        noteMap.notePacks[index].noteDatas = noteDatas.ToArray();
    }

    public void SaveNoteMapData()
    {
        string tempCode = noteMap._code;
        string json = JsonUtility.ToJson(noteMap);
        print("Saved to " + Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/" + difficultyIndex +".json");
        Directory.CreateDirectory(Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/");
        File.WriteAllText(Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/" + difficultyIndex +".json", json);

        Directory.CreateDirectory(Application.persistentDataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/");
        File.WriteAllText(Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/" + difficultyIndex +".json", json);
    }
}