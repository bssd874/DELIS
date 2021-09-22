using System.Collections.Generic;

[System.Serializable]
public class NotePack
{
    public bool comboEnabled;
    public int index;
    public NoteData[] noteDatas;
    [System.NonSerialized] public List<NoteInstance> noteInstances = new List<NoteInstance>();

    public NoteInstance[] GetNoteInstances()
    {
        for (int i = 0; i < noteDatas.Length; i++)
        {
            NoteInstance noteInstance = new NoteInstance();
            noteInstance.targetRegister = NoteRegister.current.registers[index];
            noteInstance.noteData = noteDatas[i];
            noteInstances.Add(noteInstance);
        }
        return noteInstances.ToArray();
    }
}