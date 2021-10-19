using UnityEngine.Events;
public static class GameplayData
{
    public static int highCombo;
    public static float highScore;
    public static int combo;
    public static float score;
    public static State state = new State();

    // * Set Get
    public static int _combo
    {
        set
        {
            combo = value;
            if (combo > highCombo) highCombo = combo;
            OnUpdateCombo.Invoke();
        }
        get
        {
            return combo;
        }
    }
    public static float _score
    {
        set
        {
            score = value;
            if (score > highScore) highScore = score;
            OnUpdateScore.Invoke();
        }
        get
        {
            return score;
        }
    }

    // * Events
    public static UnityEvent OnUpdateCombo = new UnityEvent();
    public static UnityEvent OnUpdateScore = new UnityEvent();

    // * Functions
    public static void Reset()
    {
        _combo = 0;
        _score = 0;
        state = new State();
    }
    public static void Validate(NoteState noteState)
    {
        int comboGathered = Combo.GetGatheredCombo(_combo, noteState);
        float scoreGathered = Score.GetGatheredScore(_combo, noteState);
        state.ApplyState(noteState);

        _combo += comboGathered;
        _score += scoreGathered;
    }

}