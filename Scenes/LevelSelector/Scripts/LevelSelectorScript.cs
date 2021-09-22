using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelectorScript : MonoBehaviour
{
    public static LevelSelectorScript main;
    public static LevelPack levelPack;
    public static LevelPack _levelPack
    {
        set
        {
            levelPack = value;
            onUpdateLevelPack.Invoke();
            onUpdateLevelData.Invoke();
            onUpdateLevelPack.Invoke();
        }
        get
        {
            if (levelPack) return levelPack;
            return ScriptableObject.CreateInstance<LevelPack>();
        }
    }
    public static LevelData levelData;
    public static LevelData _levelData
    {
        set
        {
            levelData = value;
            onUpdateNoteMapIndex.Invoke();
            onUpdateLevelData.Invoke();
            
        }
        get
        {
            if (levelData)
            {
                return levelData;
            }
            else if (levelPack)
            {
                return _levelPack.levelDatas[0];
            }
            else
            {
                return ScriptableObject.CreateInstance<LevelData>();
            }
        }
    }
    public static int notemapIndex = 0;
    public static int _notemapIndex
    {
        set
        {
            notemapIndex = value % levelData.noteMaps.Length;
            onUpdateNoteMapIndex.Invoke();
        }
        get
        {
            return notemapIndex % levelData.noteMaps.Length;
        }
    }
    public static NoteMap _noteMap
    {
        get
        {
            return _levelData.noteMaps[_notemapIndex];
        }
    }

    public AudioSource audioSource;

    // Events

    public static UnityEvent onUpdateLevelPack = new UnityEvent();
    public static UnityEvent onUpdateLevelData = new UnityEvent();
    public static UnityEvent onUpdateNoteMapIndex = new UnityEvent();

    void Awake()
    {
        main = this;

        onUpdateLevelData.AddListener(UpdateMusic);
        onUpdateLevelData.AddListener(UpdateProperties);
        onUpdateNoteMapIndex.AddListener(UpdateProperties);
    }

    void Start()
    {
        _levelPack = Database.GameData.levelPack;
    }

    public void UpdateSelector()
    {
        LevelDataSelectorScript.main.UpdatePanelData();
    }

    public void UpdateProperties()
    {

    }

    public void UpdateMusic()
    {
        if(!_levelData.audioClip) return;
        float time = audioSource.time % _levelData.audioClip.length;
        audioSource.clip = _levelData.audioClip;
        audioSource.time = time;
        if(!audioSource.isPlaying) audioSource.Play();
    }

    public void Play()
    {
        LevelSelectorNavigationModule.main.Play();
    }
}
