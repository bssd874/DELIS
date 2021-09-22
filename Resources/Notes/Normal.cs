using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{

    public GameObject Back;
    public GameObject Front;
    public GameObject Indicator1;
    public GameObject Indicator2;

    public float TimeScale = 1;

    public Note note;

    void Start()
    {
        note.StartNote();
    }

    public void Begin()
    {
        Back.LeanScale(Vector3.one * 1.2f, 0.75f * TimeScale).setEaseOutBack();
        Front.LeanScale(Vector3.one * 1f, 1f * TimeScale).setEaseOutElastic();
        Indicator1.LeanScale(Vector3.one * 1f, 1f * TimeScale).setEaseInOutCirc().setDelay(1);
        Indicator1.LeanColor(Color.green, 0.5f).setEaseInExpo().setDelay(1f);
        Back.LeanScale(Vector3.one * 1.25f, 0.5f * TimeScale).setEaseInOutCirc().setDelay(1.5f);
        Front.LeanScale(Vector3.one * 1.1f, 0.5f * TimeScale).setEaseInOutCirc().setDelay(1.5f);

        Indicator2.LeanScale(Vector3.one * 2, 0.15f).setEaseOutCirc().setDelay(1.65f);
        Indicator2.LeanScale(Vector3.one, 0.2f).setEaseInSine().setDelay(1.8f);
    }

    public void End()
    {

    }
}
