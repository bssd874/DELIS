using System.IO;
using UnityEngine;

public class JsonNoteMapPlayer : MonoBehaviour
{
    public int noteMapIndex;
    public LevelData levelData;
    public NoteRegisters noteRegisters;

    public void Start()
    {
        NoteRegisters.current = noteRegisters;
        LevelData.Initialize(levelData, noteMapIndex);
    }
}