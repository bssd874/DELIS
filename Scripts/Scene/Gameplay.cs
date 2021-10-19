using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gameplay : MonoBehaviour
{
    public static Gameplay main;
    public static LevelData levelData;
    public LevelData _levelData;
    public AudioSource audioSource;
    public NotePlayer notePlayer;

    public NoteRegister[] noteRegisters;


    private void Awake()
    {
        main = this;
        if (levelData == null) levelData = _levelData;
    }

    public static void Play(LevelData level)
    {
        Gameplay.levelData = level;
        SceneManager.LoadScene("Gameplay");
    }

    private void Start()
    {


        NoteRegister.registers = noteRegisters;

        levelData.LoadData(true);

        audioSource.clip = levelData.musicClip;
        notePlayer.noteMap = levelData.noteMap;

        Score.totalNotes = levelData.noteMap.TotalNotes();


        GameplayData.Reset();
        GameplayGUI.main.SCountdown.Countdown(4, LevelBegin);
    }
    public void LevelBegin()
    {
        UnFreeze();
        audioSource.Play();
        notePlayer.Activate();
    }
    public void LevelEnd()
    {
        LeanTween.delayedCall(3, () =>
        {
            LoadingScreen.Load(() => GameplayGUI.main.SResult.Result());
        });
    }
    public void Freeze()
    {
        audioSource.Pause();
        Time.timeScale = 0;
    }
    public void UnFreeze()
    {
        audioSource.UnPause();
        Time.timeScale = 1;
    }
}