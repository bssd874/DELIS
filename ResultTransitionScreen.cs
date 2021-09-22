using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTransitionScreen : MonoBehaviour
{
    void Start()
    {
        gameObject.LeanScaleX(2000, 0.5f);
        gameObject.LeanScaleY(1150, 3).setEaseInExpo().setOnComplete(() => gameObject.LeanAlpha(0, 1));
    }
}
