using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class LevelDataPanel : MonoBehaviour
{
    public LevelData levelData;

    [Header("References")]

    public TMP_Text Title;
    public Image BackgroundImage;
    
    public void Initialize()
    {
        Title.text = levelData.levelName;
    }

    public void Select()
    {
        Debug.Log(LevelSelectorScript.main);
        LevelSelectorScript.main.SetLevelData(levelData);
    }

}
