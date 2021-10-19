using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
//penyimpanan sebuah data dari note data (secara index)
public class NotePack
{
    public int registerIndex = 0;
    public NoteData[] datas;
    public NoteRegister register
    {
        get
        {
            return NoteRegister.registers[registerIndex];
        }
    }

    public Note[] GetNotes()
    {
        Note[] notes = new Note[datas.Length];

        for (int i = 0; i < datas.Length; i++)
        {
            notes[i] = new Note(datas[i], this);
        }

        return notes.OrderBy(n => n.globalTime).ToArray();
    }

}