using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScript : MonoBehaviour
{

    public static LevelData _levelData
    {
        get
        {
            return LevelSelectorScript.levelData;
        }
    }
    public static NoteMap _noteMap
    {
        get
        {
            return LevelSelectorScript.levelData.GetNoteMap(LevelSelectorScript._notemapIndex);
        }
    }

    public AudioSource audioSource
    {
        get
        {
            return GetComponent<AudioSource>();
        }
    }

    public NotePlayer notePlayer;
    public NoteRegister noteRegister;

    public static GameplayScript main;

    void Awake()
    {
        main = this;
    }

    void Start()
    {
        noteRegister.Select();
        
        notePlayer.Play(_levelData.audioClip, _noteMap);
        GameplayState.main.Pause();
        GameplayState.main.DisplayCountdownScreen();
    }

}
