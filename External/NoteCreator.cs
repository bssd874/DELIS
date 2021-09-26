using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteCreator : MonoBehaviour
{
    public int difficultyIndex = 0;
    public LevelData levelData;

    public NoteMap noteMap;
    public int index;
    public AudioSource audioSource;
    public LineRenderer lineRenderer;
    public List<NoteData> noteDatas;

    public RectTransform StartIndicator;

    public int samples;
    public int interval;
    public float multiplyer;
    public float distance;

    public Transform indicator;
    public float countdown;

    private void OnGUI()
    {
        if (GUILayout.Button("Insert Layer"))
        {
            InsertNoteDataIntoLayer();
        }
        if (GUILayout.Button("Convert Notemap Data"))
        {
            SaveNoteMapData();
        }
    }

    private void Start()
    {

        audioSource.clip = levelData.audioClip;

        StartIndicator.LeanScaleY(0, countdown).setOnComplete(()=> { audioSource.Play();});
    }

    private void Update()
    {
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
        noteMap.notePacks[index].noteDatas = noteDatas.ToArray();
    }

    public void SaveNoteMapData()
    {
        string json = JsonUtility.ToJson(noteMap);
        print("Saved to " + Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/" + difficultyIndex +".json");
        Directory.CreateDirectory(Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/");
        File.WriteAllText(Application.dataPath + "/Resources/Levels/" + levelData.levelName + "/NoteMaps/" + difficultyIndex +".json", json);
    }
}