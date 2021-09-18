using System.Collections.Generic;

[System.Serializable]
public class NoteMap
{
    public NotePack[] notePacks;
    public List<NoteInstance> noteInstances;
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