using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayIndicator : MonoBehaviour
{
    public Vector3 offset;
    public int currentOffset;
    public int noteOffset;
    public GameObject indicator;

    public void Indicate()
    {
        NotePlayer notePlayer = Gameplay.main.notePlayer;
        Note current = notePlayer.noteAnalyzer.notes[
                Mathf.Clamp(notePlayer.noteAnalyzer.index + currentOffset, 0, notePlayer.noteAnalyzer.notes.Length - 1)
            ];
        Note next = notePlayer.noteAnalyzer.notes[
                Mathf.Clamp(notePlayer.noteAnalyzer.index + noteOffset, 0, notePlayer.noteAnalyzer.notes.Length - 1)
            ];
        indicator.LeanMove(next.data.world, next.globalTime - current.globalTime);
    }

}
