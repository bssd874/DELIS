using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayData
{
    public static AudioClip audioClip = null;
    public static LevelResult levelResult = new LevelResult();
    public static NoteMap noteMap = null;

    

    public static class WorldData
    {
        public static int currentCombo = 0;
        public static int maxCombo = 0;
        public static int combo
        {
            set
            {
                currentCombo = value;
                if (maxCombo <= currentCombo)
                {
                    maxCombo = currentCombo;
                }
                
                onUpdate.Invoke();
            }
            get
            {
                return currentCombo;
            }
        }
        public static float score = 0;

        public static UnityEvent onUpdate = new UnityEvent();

        public static void AddScore(NoteState state = NoteState.Perfect)
        {
            int totalNotes = noteMap.noteInstances.Count;
            float P = 900000 / totalNotes;
            float C = 100000 / (totalNotes * (totalNotes - 1) / 2);

            float addedScore = 0;
            switch (state)
            {
                case NoteState.Perfect:
                    addedScore = P + (currentCombo - 1) * C;
                break;
                //-----------------------
                case NoteState.Good:
                    addedScore = (0.7f * P) + (currentCombo - 1) * C;
                break;
                //-----------------------
                case NoteState.Bad:
                    addedScore = (0.3f * P);
                break;
                //-----------------------
                case NoteState.Miss:
                    addedScore = 0;
                break;
                //-----------------------
            }
            score += addedScore;
        }

        public enum NoteState { Perfect, Good, Bad, Miss};

    }

    public static GameplayData main;
    public static void ApplyCombo(WorldData.NoteState noteState = WorldData.NoteState.Perfect)
    {
        if (noteState != WorldData.NoteState.Miss)
        {
            WorldData.combo += 1;
            WorldData.AddScore(noteState);
        }
        else
        {
            WorldData.combo = 0;
            WorldData.AddScore(noteState);
        }
    }

}
