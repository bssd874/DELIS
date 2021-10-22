using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteAnalyzer : MonoBehaviour
{
    public bool autoplay = false;
    public bool analyzing = true;

    public Note[] notes;
    public AudioSource audioSource;
    public float offset;

    public int index;
    public UnityEvent spawnCallback = new UnityEvent();
    public UnityEvent endCallback = new UnityEvent();

    Coroutine analyzeCoroutine;

    private void Start()
    {
        if (autoplay) Analyze();
    }

    public void Activate()
    {
        if (analyzeCoroutine != null) StopCoroutine(analyzeCoroutine);
        analyzeCoroutine = StartCoroutine(Analyze());
    }

    public IEnumerator Analyze()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            while (notes[i].globalTime > audioSource.time + offset) yield return null;
            if (!analyzing) yield return null;
            index = i;
            spawnCallback.Invoke();
        }
        endCallback.Invoke();
    }
}