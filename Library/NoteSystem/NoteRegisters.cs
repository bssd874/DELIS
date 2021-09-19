using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "NoteRegisters")]
public class NoteRegisters : ScriptableObject
{
    public static NoteRegisters current = null;
    public Register[] registers = null;

    public void OnEnable()
    {
        Select();
    }

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