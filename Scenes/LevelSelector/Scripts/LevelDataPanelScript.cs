using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class LevelDataPanelScript : MonoBehaviour
{
    LevelData levelData;
    public LevelData _levelData
    {
        set
        {
            levelData = value;
            UpdateInfo();
        }
        get
        {
            return levelData;
        }
    }
    public TMP_Text refTitle;

    public void UpdateInfo()
    {
        refTitle.text = _levelData.levelName;
    }

    public void AnimateWindow()
    {
        gameObject.LeanCancel();
        transform.localScale = Vector3.one;
        gameObject.LeanScale(Vector3.one * 0.9f, 1f).setEasePunch();
    }

    public void Focus()
    {
        GameObject parent = transform.parent.gameObject;
        parent.LeanCancel();
        parent.GetComponent<RectTransform>().LeanMoveX(-transform.localPosition.x, 1f).setEaseOutExpo();
    }

    public void Select()
    {
        Focus();
        AnimateWindow();
        if (levelData.noteMaps.Length == 0) return;
        LevelSelectorScript._levelData = levelData;
    }

}