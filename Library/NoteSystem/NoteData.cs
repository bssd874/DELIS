using UnityEngine;

[System.Serializable]
public class NoteData
{
    public Vector2 position;
    public float time;
    public Vector2 worldPosition
    {
        get
        {
            return Camera.main.ViewportToWorldPoint(position);
        }
    }

    public static float GetTimeInterval(NoteData a, NoteData b)
    {
        return (b.time - a.time);
    }

    

}