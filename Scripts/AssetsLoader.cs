using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader main;

    public SoundEffects soundEffects;
    public ScreenObjects screenObjects;

    public MainAssets mainAssets;
    

    public void Awake()
    {
        main = this;

    }

    public void Start()
    {
        mainAssets.noteRegister.Select();
    }


    [System.Serializable]
    public class SoundEffects
    {
        public AudioClip introSound;
    }

    [System.Serializable]
    public class ScreenObjects
    {
        public GameObject LoadingScreen;
    }

    [System.Serializable]
    public class MainAssets
    {
        public NoteRegister noteRegister;
    }

}
