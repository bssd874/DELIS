using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public static PlayerPanel main;
    public Image Ravatar;
    public TMP_Text Rname;
    void Awake()
    {
        main = this;
    }

    // Update is called once per frame
    public void UpdatePanel()
    {
        Ravatar.sprite = Game.player.avatar;
        Rname.text = Game.player.name;
    }
}
