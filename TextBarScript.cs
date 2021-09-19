using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TextBarScript : MonoBehaviour
{

    public float leanTime = 0.25f;

    public float currentValue = 0.5f;
    public float maxValue = 1;

    public TMP_Text textValue;
    public Image image;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        LeanTween.value( gameObject, 0f, currentValue / maxValue, leanTime).setEaseOutExpo().setOnUpdate( (float val)=>{image.fillAmount = val;});
    }

}
