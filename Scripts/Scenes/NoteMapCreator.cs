using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using NoteSystem.Class;

public class NoteMapCreator : MonoBehaviour
{
    
    public bool mouse = false;
    public bool touch = false;

    public string Pack = "";
    public string Data = "";
    public string Map = "";

    public AudioSource audioSource;
    public RectTransform area;
    public TrailRenderer trailRenderer;

    public int notePackIndex = 0;
    public NoteMap noteMap;
    public List<NoteData> noteDatas;

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
        noteMap.packs[notePackIndex].datas = noteDatas.ToArray();
    }

    [ContextMenu("Save NoteMap")]
    public void Save()
    {
        string directory = Application.dataPath + $"/Resources/Levels/NoteMap/{Pack}/{Data}/";
        string path = directory + $"{Map}.json";
        string json = JsonUtility.ToJson(noteMap);

        Directory.CreateDirectory(directory);
        File.WriteAllText(path, json);

        Debug.Log($"NoteMap has been saved to {path}");
    }

    public void Add(Vector2 screenPos)
    {
        if (area.rect.Contains(area.InverseTransformPoint(screenPos), true))
        {
            trailRenderer.transform.position = Camera.main.ScreenToWorldPoint(screenPos) + Vector3.forward * 1;
            NoteData noteData = new NoteData(audioSource.time, Camera.main.ScreenToViewportPoint(screenPos));
            noteDatas.Add(noteData);
        }
    }

}
