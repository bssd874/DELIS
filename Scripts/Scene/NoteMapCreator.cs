using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteMapCreator : MonoBehaviour
{
    public bool mouse = false;
    public bool touch = false;
    public static LevelData levelData;
    public LevelData _levelData;
    public int notePackIndex = 0;

    public string notePackIndexString = "0";

    [Header("Reference")]
    public AudioSource audioSource;
    public RectTransform creationArea;
    public TrailRenderer trailRenderer;

    [Header("Data")]
    public NoteMap noteMap;
    public List<NoteData> noteDatas;

    private void OnGUI()
    {
        notePackIndexString = GUILayout.TextField(notePackIndexString);
        int.TryParse(notePackIndexString, out notePackIndex);

        if (GUILayout.Button("Back"))
        {
            LevelSelector.Enter();
        }

        if (GUILayout.Button("Insert NotePack"))
        {
            InsertNotePack();
        }
        if (GUILayout.Button("Save NoteMap"))
        {
            SaveNoteMap();
        }
    }

    private void Start()
    {

        if (!levelData) levelData = _levelData;

        levelData.LoadData(true);
        audioSource.clip = levelData.musicClip;
        audioSource.Play();
    }

    public static void Create(LevelData level)
    {
        NoteMapCreator.levelData = level;
        SceneManager.LoadScene("NoteMapCreator");
    }

    void Update()
    {
        MouseInput();
        TouchInput();
    }
    public void MouseInput()
    {
        if (!mouse) return;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            Add(Input.mousePosition);
        }
    }
    public void TouchInput()
    {
        if (!touch) return;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Add(touch.position);
            }
        }
    }
    [ContextMenu("Insert NotePack")]
    public void InsertNotePack()
    {
        noteMap.notePacks[notePackIndex].datas = noteDatas.ToArray();
    }

    [ContextMenu("Save NoteMap")]
    public void SaveNoteMap()
    {
        SLS.SaveJson<NoteMap>(
            noteMap,
            Application.dataPath,
            $"/Resources/Game/Level/NoteMap/Personal Collections",
            $"{levelData.levelName}"
        );
    }

    public void Add(Vector2 screenPos)
    {
        if (creationArea.rect.Contains(creationArea.InverseTransformPoint(screenPos), true))
        {
            NoteData noteData = new NoteData(audioSource.time, Camera.main.ScreenToViewportPoint(screenPos));
            noteDatas.Add(noteData);
            trailRenderer.transform.position = Camera.main.ScreenToWorldPoint(screenPos) + Vector3.forward * 1;

        }

    }
}