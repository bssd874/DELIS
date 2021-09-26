using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader main;

    public GameObject LoadingScreen;
    public NoteRegister noteRegister;

    public void Awake()
    {
        main = this;

    }

    public void Start()
    {
        noteRegister.Select();
    }

}
