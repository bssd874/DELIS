using System.Collections.Generic;

[System.Serializable]
public class NoteMap
{
    public LevelResult levelResult;
    public NotePack[] notePacks;

    public int totalNotes
    {
        get
        {
            int total = 0;
            foreach (NotePack notePack in notePacks)
            {
                if (notePack.comboEnabled)
                {
                    total += notePack.noteDatas.Length;
                }
            }
            return total;
        }
    }

    [System.NonSerialized] public List<NoteInstance> noteInstances;
    public NoteInstance[] GetNoteInstances()
    {
        foreach (NotePack notePack in notePacks)
        {
            noteInstances.AddRange(notePack.GetNoteInstances());
        }

        noteInstances.Sort();

        return noteInstances.ToArray();
    }
}