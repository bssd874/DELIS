using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Note
{
    public NoteData data;
    public NotePack pack;


    public Note(NoteData noteData, NotePack notePack)
    {
        data = noteData;
        pack = notePack;
    }

    public float globalTime
    {
        get
        {
            return data.time + (pack.register.offset);
        }
    }

}