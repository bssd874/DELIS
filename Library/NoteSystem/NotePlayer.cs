using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NotePlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public NoteInstance[] noteInstances;
    public int spawnIndex = 0;
    public int hitIndex = 0;

    public UnityEvent onActivate = new UnityEvent();

    public Coroutine SpawnerCoroutine;
    public Coroutine HitCorountine;

    public void Play(AudioClip audioClip, NoteMap noteMap)
    {
        audioSource.clip = audioClip;
        noteInstances = noteMap.GetNoteInstances();
        Activate();
    }

    public void Activate()
    {
        SpawnerCoroutine = StartCoroutine(Spawner());
        HitCorountine = StartCoroutine(HitNotesIndex());
        onActivate.Invoke();
    }

    private IEnumerator Spawner()
    {
        StartCoroutine(HitNotesIndex());

        audioSource.Play();

        if (noteInstances == null) yield break;

        for (int i = 0; i < noteInstances.Length; i++)
        {
            NoteInstance noteInstance = noteInstances[i];
            while (noteInstance.spawnTime > audioSource.time) yield return null;
            SpawnNoteInstance(noteInstance);
            spawnIndex = i;
        }
        GameplayData.Data.EvaluateResult();
        yield return true;
    }

    private IEnumerator HitNotesIndex()
    {
        if (noteInstances == null) yield break;

        for (int i = 0; i < noteInstances.Length; i++)
        {
            NoteInstance noteInstance = noteInstances[i];
            while (noteInstance.noteData.time > audioSource.time) yield return null;
            hitIndex = i;
        }

        yield return true;
    }

    public void SpawnNoteInstance(NoteInstance noteInstance)
    {
        
        GameObject obj = Instantiate(noteInstance.instance, noteInstance.noteData.worldPosition, Quaternion.identity, transform);
    }

    

}