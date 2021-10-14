using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NoteSystem.Class;
using UnityEngine;

public class NoteMapCreator : MonoBehaviour
{

    public bool mouse = false;
    public bool touch = false;
    public static LevelData levelData;
    public LevelData localLevelData;
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
    private void OnGUI()
    {
        mapName = GUILayout.TextField(mapName);
    }

    void Start()
    {

        if (!levelData) levelData = localLevelData;
        if (!levelData) return;

        AudioClip audio = levelData.data.audioClip;

        if (!audio) audio = LP.Module.LoadMusic(
                    levelData.info.levelPack,
                    levelData.info.name,
                    levelData.info.music
                    );
        if (!audio) return;
        audioSource.clip = audio;
        audioSource.Play();
    }

    public static void Create(LevelData data)
    {
        levelData = data;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
    }

    void Update()
    {
        if (!audioSource || !audioClip)
            if (audioSource.time >= audioSource.clip.length) UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector");
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
        string mapFolder = sector + $"Maps/{levelData.info.levelPack}/{levelData.info.name}/{levelData.info.music}/";
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
