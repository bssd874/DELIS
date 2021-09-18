using UnityEngine;

public class Note_Normal : MonoBehaviour
{
    public Note note;

    public void Start()
    {
        note.StartNote();
    }

    public void ApplyCombo()
    {
        GameplayData.ApplyCombo();
    }

}