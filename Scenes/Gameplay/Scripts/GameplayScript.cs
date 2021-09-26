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
            return LevelSelectorScript.levelData.GetNoteMapIndex(LevelSelectorScript._notemapIndex);
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

    public TMPro.TMP_Text combo;
    public TMPro.TMP_Text score;

    public static GameplayScript main;

    NoteAnalyzer analyzer;

    void Awake()
    {
        main = this;
    }

    

    void Start()
    {
        noteRegister.Select();

        analyzer = new NoteAnalyzer(notePlayer);
        analyzer.onUpdate.AddListener(Hit);
        analyzer.onEnd.AddListener(End);
        GameplayData.Data.onCombo.AddListener(UpdateInfo);
        
        notePlayer.Play(_levelData.audioClip, _noteMap);
        GameplayState.main.Pause();
        GameplayState.main.DisplayCountdownScreen();
    }

    void Hit()
    {
        Debug.DrawLine(analyzer.noteData.worldPosition, analyzer.GetNoteDataOffset(1).worldPosition, Color.green, analyzer.GetNoteDataOffset(1).time - analyzer.GetNoteDataOffset(0).time);
    }

    void End()
    {
        _levelData.SaveNoteMap(_noteMap);
        LeanTween.delayedCall(3, ChangeScene);
    }

    void ChangeScene()
    {
        LoadingScreenScript.LoadingScreen(() => UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelector"), 1);
    }

    void UpdateInfo()
    {
        combo.text = GameplayData.Data.combo.ToString();
        score.text = string.Format("{0:000000}", GameplayData.Data._score);
    }

}
