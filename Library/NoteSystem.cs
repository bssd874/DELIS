using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

using NoteSystem.Class;

namespace NoteSystem.Class
{
    //bentuk asli dari objek
    public struct Note
    {
        public NoteData data;
        public NotePack pack;

        
        public Note(NoteData noteData, NotePack notePack)
        {
            data = noteData;
            pack = notePack;
        }

        public float globalTime
        {
            get
            {
                return data.time + pack.register.offset;
            }
        }

    }

    [System.Serializable]
    //untuk menyimpan posis dan waktu
    public struct NoteData
    {
        public float time;
        public Vector2 viewport;

        public NoteData(float time, Vector2 viewport = new Vector2())
        {
            this.time = time;
            this.viewport = viewport;
        }

        public Vector2 world
        {
            get
            {
                return Camera.main.ViewportToWorldPoint(viewport);
            }
        }
    }

    [System.Serializable]
    //penyimpanan sebuah data dari note data (secara index)
    public class NotePack
    {
        public int registerIndex = 0;
        public NoteData[] datas;
        public bool count = true;
        public NoteRegister register
        {
            get
            {
                return NoteRegister.registers[registerIndex];
            }
        }

        public Note[] GetNotes()
        {
            Note[] notes = new Note[datas.Length];

            for (int i = 0; i < datas.Length; i++)
            {
                notes[i] = new Note(datas[i], this);
            }

            return notes.OrderBy(n => n.globalTime).ToArray();
        }

    }

    [System.Serializable]
    //penyimpanan note pack
    public class NoteMap
    {
        public NotePack[] packs;
        public Data data;

        public class Data
        {
            public string ID = "";
            public Result result = new Result();
        }

        public class Result
        {
            
        }

        public static Note[] GetNotes(NoteMap noteMap)
        {
            List<Note> notes = new List<Note>();

            for (int i = 0; i < noteMap.packs.Length; i++)
            {
                notes.AddRange(noteMap.packs[i].GetNotes());
            }

            return notes.OrderBy(n => n.globalTime).ToArray();
        }

        public static int GetTotalNotes(NoteMap noteMap)
        {
            int total = 0;
            for (int i = 0; i < noteMap.packs.Length; i++)
            {
                total += noteMap.packs[i].datas.Length;
            }
            return total;
        }

    }

    [System.Serializable]
    public class NoteRegister
    {

        public static NoteRegister[] registers;

        public GameObject instance;
        public float offset = 0;
    }

}

namespace NoteSystem.Manager
{
    //notifikasi
    public class NoteAnalyzer 
    {
        public bool analyzing = true;

        public Note[] notes;
        public AudioSource audioSource;
        public float offset;

        public int index;
        public UnityEvent StartCallback = new UnityEvent();
        public UnityEvent eventCallback = new UnityEvent();
        public UnityAction actionCallback;
        public UnityEvent EndCallback = new UnityEvent();
        Coroutine analyzerCorotine;

        public NoteAnalyzer(Note[] notes, AudioSource audioSource)
        {
            this.notes = notes;
            this.audioSource = audioSource;
        }

        public void Activate(MonoBehaviour sender)
        {
            analyzerCorotine = sender.StartCoroutine(Analyze());
        }

        public IEnumerator Analyze()
        {
            StartCallback.Invoke();
            for (int i = 0; i < notes.Length; i++)
            {
                while (notes[i].globalTime > audioSource.time + offset) yield return null;
                if (!analyzing) yield return null;
                index = i;
                eventCallback.Invoke();
                actionCallback?.Invoke();
            }
            EndCallback.Invoke();
        }
    }

    //memainkan note map
    [System.Serializable]
    public class NotePlayer
    {
        public NoteAnalyzer analyzer;

        public Note[] notes;
        public AudioSource audioSource;

        public void Activate(MonoBehaviour sender, GameObject parent)
        {
            analyzer = new NoteAnalyzer(notes, audioSource);
            analyzer.Activate(sender);
            analyzer.actionCallback = () => Spawn(parent, notes[analyzer.index]);
        }

        public void Spawn(GameObject parent, Note note)
        {
            GameObject gameObject = GameObject.Instantiate(note.pack.register.instance, (Vector3)note.data.world + Vector3.forward * 100, Quaternion.identity, parent.transform);
            gameObject.LeanMoveLocalZ(0, 1);
        }
    }

    //Creator
    [System.Serializable]
    public class NoteCreator
    {
        public AudioSource audioSource;

        public NoteMap noteMap;
        public int packArraySize = 5;

        public NoteCreator(AudioSource audioSource)
        {
            this.audioSource = audioSource;
            Initialize();
        }

        public void Initialize()
        {
            noteMap = new NoteMap();
            noteMap.packs = new NotePack[packArraySize];
        }

        public void InsertNotePack(NotePack notePack, int index = 0)
        {
            noteMap.packs[Mathf.Clamp(index, 0, noteMap.packs.Length)] = notePack;
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.dataPath + "/a.json", json);
        }

        //untuk bikin note map
        [System.Serializable]
        public class Creator
        {
            public List<NoteData> datas = new List<NoteData>();
            public AudioSource audioSource;

            public Creator(AudioSource audioSource)
            {
                this.audioSource = audioSource;
            }

            public void Add(Vector2 viewport)
            {
                NoteData noteData = new NoteData(audioSource.time, viewport);
                datas.Add(noteData);
            }

            public void Clear()
            {
                datas.Clear();
            }

            public NotePack GetPack()
            {
                NotePack notePack = new NotePack();
                notePack.datas = datas.ToArray();
                return notePack;
            }

        }

    }

}