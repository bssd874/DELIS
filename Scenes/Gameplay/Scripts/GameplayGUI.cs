using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayGUI : MonoBehaviour
{
    public TMP_Text combo;
    public TMP_Text score;

    public int currentScore = 0;

    void Start()
    {
        GameplayData.Data.onCombo.AddListener(UpdateCombo);
    }

    public void UpdateCombo()
    {
        combo.text = GameplayData.Data.combo.ToString();
        combo.rectTransform.LeanCancel();
        combo.rectTransform.localScale = Vector3.one;
        combo.rectTransform.LeanScale(Vector3.one * 0.5f, 0.5f).setEasePunch();
        UpdateScore();
    }

    public void UpdateScore()
    {
        score.text = GameplayData.Data.score.ToString();
    }
}