using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteSystem.Class;
using NoteSystem.Manager;
using UnityEngine.Events;

using TMPro;

public class Gameplay : MonoBehaviour
{
    // Core
    public static LevelData levelData;
    public static int notemapIndex = 0;

    public static NoteMap noteMap
    {
        get
        {
            return levelData.noteMaps[notemapIndex];
        }
    }

    public GameplayGUI gameplayGUI;
    public NoteRegister[] noteRegisters;
    public NotePlayer notePlayer;
    public GameObject noteArea;


    // Default

    void Start()
    {
        Initialize();
    }

    // Main

    public void Initialize()
    {
        GameplayData.Stats.comboUpdate.AddListener(gameplayGUI.UpdateCombo);
        GameplayData.Stats.comboUpdate.AddListener(gameplayGUI.UpdateScore);


        NoteRegister.registers = noteRegisters;

        notePlayer.notes = NoteMap.GetNotes(noteMap);
        ScoreSystem.totalNotes = NoteMap.GetTotalNotes(noteMap);

        notePlayer.audioSource.Stop();
        notePlayer.audioSource.clip = LevelSelector.levelData.data.audioClip;
        Begin();

        notePlayer.Activate(this, noteArea);
    }

    // Function

    public void Begin()
    {
        notePlayer.audioSource.Play();
    }

    public void End()
    {

    }

    // GUI Command

}

[System.Serializable]
public class GameplayGUI
{
    public TMP_Text combo;
    public TMP_Text score;

    public void UpdateCombo()
    {
        combo.text = GameplayData.Stats.combo.ToString();
    }

    public void UpdateScore()
    {
        score.text = string.Format("{0:000000}", ((int)GameplayData.Stats.score).ToString()) ;
    }

}

public class GameplayData
{

    public class Stats
    {
        public static int combo = 0;
        public static float score = 0;

        public static UnityEvent comboUpdate = new UnityEvent();
        public static UnityEvent scoreUpdate = new UnityEvent();

        public class ComboInfo
        {
            public static int Perfect = 0;
            public static int Good = 0;
            public static int Bad = 0;
            public static int Miss = 0;

            public static void Reset()
            {
                Perfect = 0;
                Good = 0;
                Bad = 0;
                Miss = 0;
            }

        }

        public static void Reset()
        {
            combo = 0;
            score = 0;
            ComboInfo.Reset();
        }

    }

    public void Initialize()
    {
        Stats.Reset();
        
    }
}