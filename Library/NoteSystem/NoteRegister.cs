using UnityEngine;

[System.Serializable]
public class NoteRegister
{
    public static NoteRegister current = null;
    public Register[] registers = null;

    public void Select()
    {
        current = this;
    }

    [System.Serializable]
    public class Register
    {
        public GameObject gameObject;
        public float offset;
    }
}