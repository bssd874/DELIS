public class State
{

    public int perfect = 0;
    public int good = 0;
    public int bad = 0;
    public int miss = 0;

    public void ApplyState(NoteState noteState)
    {
        switch (noteState)
        {
            case NoteState.Perfect:
                perfect += 1;
                break;
            case NoteState.Good:
                good += 1;
                break;
            case NoteState.Bad:
                bad += 1;
                break;
            case NoteState.Miss:
                miss += 1;
                break;
        }
    }
}