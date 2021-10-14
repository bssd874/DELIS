using System;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.Events;

public class LoadingScreen
{
    public static GameObject prefabScreen;
    public GameObject window;

    public ReferencePaths p;
    public ReferenceObjects r;

    public UnityAction action;
    public float startDuration = 1.5f;
    public float loadingTime = 1;
    public float endDuration = 1.5f;


    public LoadingScreen(UnityAction action)
    {
        this.action = action;
        window = GameObject.Instantiate(prefabScreen);
        GameObject.DontDestroyOnLoad(window);
        r = new ReferenceObjects(window);
        LoadingProcess();
    }

    public class ReferencePaths
    {
        public static string animator = "Loading";
    }

    public class ReferenceObjects
    {

        public Animator animator;

        public ReferenceObjects(GameObject o)
        {
            animator = o.GetReference<Animator>(ReferencePaths.animator);
        }
    }

    public static LoadingScreen Load(UnityAction action)
    {
        return new LoadingScreen(action);
    }

    public void LoadingProcess()
    {
        LeanTween.delayedCall(0, () => r.animator.Play("In"));
        LeanTween.delayedCall(startDuration + loadingTime / 2, () => action.Invoke());
        LeanTween.delayedCall(startDuration + loadingTime, () => r.animator.Play("Out"));
        LeanTween.delayedCall(startDuration + loadingTime + endDuration, () => GameObject.Destroy(window));
    }

}
