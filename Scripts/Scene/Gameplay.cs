using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gameplay : MonoBehaviour
{
    public static float delay = 0;
    public static Gameplay main;
    public static LevelData levelData;
    public LevelData _levelData;
    public AudioSource audioSource;
    public NotePlayer notePlayer;

    public NoteRegister[] noteRegisters;

    public LTDescr levelCompleteLT;


    private void Awake()
    {
        main = this;
    }

    public static void Play(LevelData level, float delay = -1)
    {
        if (delay < 0) delay = LoadingScreen.processDuration / 2;
        Gameplay.delay = delay;
        Gameplay.levelData = level;
        SceneManager.LoadScene("Gameplay");
    }

    private void Start()
    {

        if (levelData == null) levelData = _levelData;
        LeanTween.delayedCall(
            delay,
            StartGame
        );
    }

    public void StartGame()
    {
        NoteRegister.registers = noteRegisters;

        levelData.LoadData(true);

        audioSource.clip = levelData.musicClip;
        notePlayer.noteMap = levelData.noteMap;

        Score.totalNotes = levelData.noteMap.TotalNotes();

        if (Game.player.autohit) notePlayer.autohit = true;

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
        levelCompleteLT = LeanTween.delayedCall(3, () =>
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

    public void Restart()
    {
        Time.timeScale = 1;
        LoadingScreen.Load(() => Play(levelData));
    }
    public void Back()
    {
        if (levelCompleteLT != null) LeanTween.cancel(levelCompleteLT.id);
        Time.timeScale = 1;

        LevelPack levelPack = LevelSelector.levelPack;

        if (levelPack) LoadingScreen.Load(() => { LevelSelector.Enter(levelPack); });
    }
}