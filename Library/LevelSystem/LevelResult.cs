using System.IO;
using UnityEngine;

public class LevelResult
{
    public int highCombo = 0;
    public int highScore = 0;
    public float skill = 0;
    public int timesPlayed = 0;
    public NoteRank rank
    {
        get
        {
            return GetRank(highScore);
        }
    }

    public static NoteRank GetRank(int Score)
    {
        NoteRank r = NoteRank.F;

        if (Score >= 1000000) r = NoteRank.S; else
        if (Score >= 950000) r = NoteRank.A; else
        if (Score >= 880000) r = NoteRank.B; else
        if (Score >= 800000) r = NoteRank.C; else
        r = NoteRank.F;
        
        return r;
    }

    
}

public enum NoteRank {S, A, B, C, F};