using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class NoteAnalyzer
    {

        public NoteData noteData;
        public float timeOffset = 0;
        public int noteIndex = 0;
        

        public UnityEvent onStart = new UnityEvent();
        public UnityEvent onUpdate = new UnityEvent();
        public UnityEvent onEnd = new UnityEvent();

        
        public NotePlayer notePlayer;
        public Coroutine coroutine;

        public NoteAnalyzer(NotePlayer notePlayer, float timeOffset = 0)
        {
            this.notePlayer = notePlayer;
            this.timeOffset = timeOffset;
            notePlayer.onActivate.AddListener(Analyze);
        }

        public void Analyze()
        {
            this.coroutine = this.notePlayer.StartCoroutine(AnalyzeCoroutine());
        }

        public NoteData GetNoteData(int index)
        {
            return notePlayer.noteInstances[Mathf.Clamp((index), 0, notePlayer.noteInstances.Length)].noteData;
        }

        public NoteData GetNoteDataOffset(int indexOffset)
        {
            return notePlayer.noteInstances[Mathf.Clamp((noteIndex + indexOffset), 0, notePlayer.noteInstances.Length - 1)].noteData;
        }

        IEnumerator AnalyzeCoroutine()
        {
            if (notePlayer.noteInstances == null) yield return false;
            onStart.Invoke();
            for (int i = 0; i < notePlayer.noteInstances.Length; i++)
            {
                NoteInstance noteInstance = notePlayer.noteInstances[i];
                while ((noteInstance.noteData.time + timeOffset) > notePlayer.audioSource.time) yield return null;
                noteData = noteInstance.noteData;
                noteIndex = i;
                onUpdate.Invoke();
            }
            onEnd.Invoke();
            yield return true;
        }

    }