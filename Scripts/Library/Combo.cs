public class Combo
{

    public static int GetGatheredCombo(int currentCombo, NoteState noteState)
    {
        int comboGathered = 0;
        switch (noteState)
        {
            case NoteState.Perfect:
                comboGathered = 1;
                break;
            case NoteState.Good:
                comboGathered = 1;
                break;
            case NoteState.Bad:
                comboGathered = -currentCombo;
                break;
            case NoteState.Miss:
                comboGathered = -currentCombo;
                break;
        }
        return comboGathered;
    }
}