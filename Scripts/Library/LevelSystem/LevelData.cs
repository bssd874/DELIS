using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "LevelSystem/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName = "default";
    public string musicName = "default";
    public string descriptions;
    public string source = "Lagu Daerah Indonesia";

    public string packName = "default";

    private void OnValidate()
    {
        levelName = name;
    }

    [ContextMenu("Load")]
    public void Load()
    {
        this.LoadData();
    }

    public LevelResult levelResult = new LevelResult();
    public NoteMap noteMap;
    [NonSerialized] public AudioClip musicClip;
    [NonSerialized] public Sprite levelSprite;
}
