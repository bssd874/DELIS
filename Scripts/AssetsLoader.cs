using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader main;

    public GameObject LoadingScreen;

    public void Awake()
    {
        main = this;
    }
}
