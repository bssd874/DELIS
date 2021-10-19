using System.Collections.Generic;
using System.Linq;
public static class NoteMapExtensions
{
    public static Note[] Notes(this NoteMap noteMap)
    {
        List<Note> notes = new List<Note>();

        for (int i = 0; i < noteMap.notePacks.Length; i++)
        {
            notes.AddRange(noteMap.notePacks[i].GetNotes());
        }

        return notes.OrderBy(n => n.globalTime).ToArray();
    }

    public static int TotalNotes(this NoteMap noteMap)
    {
        int total = 0;
        for (int i = 0; i < noteMap.notePacks.Length; i++)
        {
            total += noteMap.notePacks[i].datas.Length;
        }
        return total;
    }
}