using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDataPanel : MonoBehaviour
{
    public LevelData levelData;
    public TMP_Text RName;
    public TMP_Text RMusic;

    public void Initialize()
    {
        RName.text = levelData.levelName;
        RMusic.text = levelData.musicName;
    }

}
