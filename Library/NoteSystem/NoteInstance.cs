using System;
using UnityEngine;

[System.Serializable]
public class NoteInstance : IComparable
{
    public NoteRegister.Register targetRegister;

    public GameObject instance
    {
        get
        {
            return targetRegister.gameObject;
        }
    }

    public NoteData noteData;

    public float spawnTime
    {
        get
        {
            return noteData.time + targetRegister.offset;
        }
    }

    public int CompareTo(object obj)
    {
        return (noteData.time).CompareTo((obj as NoteInstance).noteData.time);
    }
}