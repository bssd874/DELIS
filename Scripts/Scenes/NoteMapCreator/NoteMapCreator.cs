using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using NoteSystem.Class;

public class NoteMapCreator : MonoBehaviour
{
    
    public bool mouse = false;
    public bool touch = false;

    public LevelPack levelPack;
    public LevelData levelData;
    public AudioClip audioClip;
    public string mapName = "";
    public int notePackIndex = 0;

    [Header("References")]
    public AudioSource audioSource;
    public RectTransform area;
    public TrailRenderer trailRenderer;

    [Header("Data")]
    public NoteMap noteMap;
    public List<NoteData> noteDatas;

    void Start()
    {
        if (levelData.info.music == "") levelData.info.music = audioClip.name;

        AudioClip audio = levelData.data.audioClip;
        
        if (!audio) audio = LP.Module.LoadMusic(
                    levelPack.info.name,
                    levelData.info.name,
                    levelData.info.music
                    );

        audioSource.clip = audio;
        audioSource.Play();
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
        noteMap.packs[notePackIndex].datas = noteDatas.ToArray();
    }

    [ContextMenu("Save NoteMap")]
    public void Save()
    {
        

        string sector = Application.dataPath + $"/Resources/Levels/";
        string mapFolder = sector + $"Maps/{levelPack.info.name}/{levelData.info.name}/{levelData.info.music}/";
        string filePath = mapFolder + $"{mapName}.json";

        string json = JsonUtility.ToJson(noteMap);

        Directory.CreateDirectory(mapFolder);

        File.WriteAllText(filePath, json);

        Debug.Log($"NoteMap has been saved to {filePath}");
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
