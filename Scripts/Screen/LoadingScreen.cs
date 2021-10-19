using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static GameObject ScreenPrefab;
    public GameObject screen;
    public Animator animator;
    public Action action;

    public bool done;

    public string InName = "In";
    public string OutName = "Out";

    public static void Load(Action targetAction)
    {
        GameObject window = Instantiate(ScreenPrefab);
        DontDestroyOnLoad(window);

        LoadingScreen loadingScreen = window.GetComponent<LoadingScreen>();

        loadingScreen.action = targetAction;
        loadingScreen.screen = window;

        loadingScreen.Loading();
    }

    public void Loading()
    {
        animator.Play(InName);
    }

    public void InvokeAction()
    {
        StartCoroutine(LoadingProcess());
    }

    public void DestroyScreen()
    {
        Destroy(gameObject);
    }

    public IEnumerator LoadingProcess()
    {
        bool complete = false;
        while (!complete)
        {
            action?.Invoke();
            complete = true;
            yield return new WaitForSeconds(1);
        }
        done = true;
        animator.Play(OutName);
    }

}
