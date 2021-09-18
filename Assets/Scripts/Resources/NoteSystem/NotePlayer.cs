using System.Collections;
using UnityEngine;

public class NotePlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public NoteInstance[] noteInstances;
    public int spawnIndex = 0;
    public int hitIndex = 0;

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
        Instantiate(noteInstance.instance, noteInstance.noteData.worldPosition, Quaternion.identity, transform);
    }
}