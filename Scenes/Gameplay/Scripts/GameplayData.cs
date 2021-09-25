using UnityEngine;
using UnityEngine.Events;
public static class GameplayData
{
    public class Data
    {
        public static LevelResult _levelResult
        {
            get
            {
                return GameplayScript._noteMap.levelResult;
            }
        }
        public static int _totalNotes
        {
            get
            {
                return GameplayScript._noteMap.totalNotes;
            }
        }
        public static float P
        {
            get
            {
                return 900000f / _totalNotes;
            }
        }
        public static float C
        {
            get
            {
                return 100000f / (_totalNotes * (_totalNotes - 1) / 2);
            }
        }
        public static float bonusScore
        {
            get
            {
                return (combo) * C;
            }
        }


        public static int combo;
        public static float score;
        public static int _score
        {
            get
            {
                return Mathf.RoundToInt(score);
            }
        }
        public static int skill;
        public static UnityEvent onCombo = new UnityEvent();
        public static UnityEvent onScore = new UnityEvent();
        public static UnityEvent onEvaluate = new UnityEvent();

        public static class StateEvaluation
        {
            public static int perfect = 0;
            public static int good = 0;
            public static int bad = 0;
            public static int miss = 0;

            public static float result
            {
                get
                {
                    return ((perfect * 100) + (good * 70) + (bad * 30)) / 2;
                }
            }

        }

        public static void ApplyCombo(NoteState noteState = NoteState.Perfect)
        {
            float scoreAmount = 0;
            int comboAmount = 1;
            switch (noteState)
            {
                case NoteState.Perfect:
                    scoreAmount = P + bonusScore;
                    StateEvaluation.perfect += 1;
                break;
                //=========================
                case NoteState.Good:
                    combo += 1;
                    scoreAmount = (0.7f * P) + bonusScore;
                    StateEvaluation.good += 1;
                break;
                //=========================
                case NoteState.Bad:
                    scoreAmount = (0.3f * P) + bonusScore;
                    StateEvaluation.bad += 1;
                break;
                //=========================
                case NoteState.Miss:
                    combo = 0;
                    comboAmount = 0;
                    scoreAmount = 0;
                    StateEvaluation.miss += 1;
                break;
                //=========================
            }
            combo += comboAmount;
            score += scoreAmount;
            onCombo.Invoke();
            onScore.Invoke();
        }

        public static void EvaluateResult()
        {
            if (combo > _levelResult.highCombo)
            {
                _levelResult.highCombo = combo;
            }
            if (score > _levelResult.highScore)
            {
                _levelResult.highScore = _score;
            }
            float stateResult = StateEvaluation.result;
            if (stateResult > _levelResult.skill)
            {
                _levelResult.skill = stateResult;
            }
            onEvaluate.Invoke();
        }
    }
    

}