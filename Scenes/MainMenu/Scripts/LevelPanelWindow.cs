using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class LevelPanelWindow : MonoBehaviour
{
    public LevelData levelData;

    [Header("References")]

    public TMP_Text Title;
    public TMP_Text Rank;

    public void Initialize()
    {
        Title.text = levelData.levelName;
    }
}
