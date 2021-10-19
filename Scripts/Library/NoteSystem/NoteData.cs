using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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