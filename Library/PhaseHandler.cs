using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PhaseManager))]
public class PhaseHandler : MonoBehaviour
{
    public int defaultIndex = -1;
    public PhaseContainer[] phaseContainers;

    public void Start()
    {
        foreach (PhaseContainer phaseContainer in phaseContainers)
        {
            Array.Sort(phaseContainer.notePhases.ToArray());
        }
        if (defaultIndex >= 0)
        {
            ActivatePhaseIndex(defaultIndex);
        }
    }

    public void ActivatePhaseIndex(int index)
    {
        if (index >= phaseContainers.Length) return;
        PhaseContainer phaseContainer = phaseContainers[index];

        phaseContainer.coroutine = StartCoroutine(StartPhase(phaseContainer));
    }

    public void StopPhaseIndex(int index)
    {
        if (index >= phaseContainers.Length) return;
        PhaseContainer phaseContainer = phaseContainers[index];

        StopCoroutine(phaseContainer.coroutine);
    }

    public void clearPhaseIndex(int index)
    {
        if (index >= phaseContainers.Length) return;
        PhaseContainer phaseContainer = phaseContainers[index];

        foreach (GameObject instance in phaseContainer.instances)
        {
            Destroy(instance);
        }
    }

    public void DeactivatePhaseIndex(int index)
    {
        if (index >= phaseContainers.Length) return;

        PhaseContainer phaseContainer = phaseContainers[index];

        StopPhaseIndex(index);
        clearPhaseIndex(index);
    }

    public IEnumerator StartPhase(PhaseContainer phaseContainer)
    {
        NotePhase[] notePhases = phaseContainer.notePhases.ToArray();

        StartCoroutine(phaseContainer.startTimeline());

        Coroutine coroutine = null;
        foreach (NotePhase notePhase in notePhases)
        {
            while (notePhase.spawnTime > phaseContainer.timeline) yield return null;
            coroutine = StartCoroutine(DeployPhase(phaseContainer, notePhase));
        }
        yield return coroutine;
    }

    public IEnumerator DeployPhase(PhaseContainer phaseContainer, NotePhase notePhase)
    {
        notePhase.OnStart.Invoke();

        GameObject instance = DeployInstance(notePhase.instance, notePhase.spawnGlobal, notePhase.instanceLifetime);

        phaseContainer.instances.Add(instance);
        yield return new WaitForSeconds(notePhase.switchDuration);
        notePhase.OnEnd.Invoke();
        yield return null;
    }

    public GameObject DeployInstance(GameObject instance, bool global = true, float lifetime = -1, bool recordInstances = true)
    {
        if (!instance) return null;
        GameObject instanceObject;
        if (global)
        {
            instanceObject = Instantiate(instance, transform.position, transform.rotation);
        }
        else
        {
            instanceObject = Instantiate(instance, transform.position, transform.rotation, transform);
        }

        if (0 <= lifetime)
        {
            PhaseManager.main.StartCoroutine(OptimizedDestroy(instanceObject, lifetime));
        }
        return instanceObject;
    }

    public IEnumerator OptimizedDestroy(GameObject instance, float time = 0)
    {
        float timeline = 0;

        while (timeline < time)
        {
            timeline += Time.deltaTime;
            yield return null;
        }

        Destroy(instance);
    }

    [System.Serializable]
    public class NotePhase : IComparable
    {
        public GameObject instance;
        public bool spawnGlobal;
        public float spawnTime = 0;
        public float instanceLifetime = -1;
        public float switchDuration = 0;

        public UnityEvent OnStart;
        public UnityEvent OnEnd;

        public int CompareTo(object obj)
        {
            return (spawnTime).CompareTo((obj as NotePhase).spawnTime);
        }
    }

    [System.Serializable]
    public class PhaseContainer
    {
        public List<NotePhase> notePhases;
        [System.NonSerialized] public List<GameObject> instances = new List<GameObject>();
        [System.NonSerialized] public Coroutine coroutine;
        [System.NonSerialized] public float timeline;

        public IEnumerator startTimeline()
        {
            while (true)
            {
                timeline += Time.deltaTime;
                yield return null;
            }
        }
    }
}