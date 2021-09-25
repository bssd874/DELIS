using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject LoadingScreenObject;
    public static LoadingScreenManager main;
    public void Awake()
    {
        main = this;
    }
    public void LoadingScreen(float duration, UnityAction action)
    {
        GameObject screen = GameObject.Instantiate(LoadingScreenObject);
        DontDestroyOnLoad(screen);
        screen.GetComponent<LoadingScreenScript>().Initialize(duration, action);
        
    }
}