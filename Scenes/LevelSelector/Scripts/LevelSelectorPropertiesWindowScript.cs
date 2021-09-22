using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LevelSelectorPropertiesWindowScript : MonoBehaviour
{

    public static LevelResult _levelResult
    {
        get
        {
            return LevelSelectorScript._noteMap.levelResult;
        }
    }

    public References references;

    void Start()
    {
        LevelSelectorScript.onUpdateNoteMapIndex.AddListener(UpdateWindow);
    }

    public void UpdateWindow()
    {
        AnimateWindow();
        UpdateInfo();
    }

    public void AnimateWindow()
    {
        references.windowGroup.gameObject.LeanCancel();
        references.windowGroup.alpha = 0;
        references.windowGroup.LeanAlpha(1, 1f).setEaseOutExpo();
        references.windowGroup.transform.localScale = Vector3.one * 0.975f;
        references.windowGroup.transform.gameObject.LeanScale(Vector3.one, 0.5f).setEaseOutExpo();
    }

    public void UpdateInfo()
    {
        Debug.Log(LevelSelectorScript._levelPack);
        Debug.Log(LevelSelectorScript._levelData.levelName);
        references.Title.text = LevelSelectorScript._levelData.levelName;
        references.combo.text = _levelResult.highCombo.ToString();
        references.score.text = _levelResult.highScore.ToString();
        references.skill.text = _levelResult.skill.ToString();
        references.timesPlayed.text = _levelResult.timesPlayed.ToString();
        references.rank.text = _levelResult.rank.ToString();
        references.stage.text = LevelSelectorScript._notemapIndex.ToString();
    }

    public void AddStage()
    {
        LevelSelectorScript._notemapIndex += 1;
    }

    [System.Serializable]
    public class References
    {
        public CanvasGroup windowGroup;
        public TMP_Text Title;
        public TMP_Text combo;
        public TMP_Text score;
        public TMP_Text skill;
        public TMP_Text timesPlayed;

        public TMP_Text rank;
        public TMP_Text stage;
    }

}