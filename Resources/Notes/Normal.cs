using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    public NoteState state = NoteState.Miss;
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
        Indicator1.LeanScale(Vector3.one * 0.75f, 0.5f * TimeScale).setEaseOutCirc().setDelay(0.75f);
        Indicator1.LeanScale(Vector3.one * 1.75f, 0.4f * TimeScale).setEaseOutCirc().setDelay(1.6f);
        Indicator1.LeanColor(Color.yellow, 0.75f).setEaseInOutSine().setDelay(1f);
        Indicator1.LeanColor(Color.green, 0.25f).setEaseInOutSine().setDelay(1.75f);
        Back.LeanScale(Vector3.one * 2f, 0.2f * TimeScale).setEaseOutBack().setDelay(1.5f);
        Front.LeanScale(Vector3.one * 1.75f, 0.2f * TimeScale).setEaseOutBack().setDelay(1.5f);

        Indicator2.LeanScale(Vector3.one * 4f, 0.15f).setEaseOutCirc().setDelay(1.65f);
        Indicator2.LeanScale(Vector3.one * 1.25f, 0.2f).setEaseInSine().setDelay(1.8f);

        Indicator1.LeanColor(Color.white, 0).setEaseInSine().setDelay(1.95f);

    }

    public void AddCombo()
    {
        GameplayData.Data.ApplyCombo(state);
    }

    public void SetState(int index)
    {
        state = (NoteState)index;
    }

    public void Explode()
    {
        
        Front.LeanCancel();
        Back.LeanCancel();
        Indicator1.LeanCancel();
        Indicator2.LeanCancel();
        Back.LeanColor(Color.white, 0);
        Back.LeanAlpha(0.25f, 0);

        Indicator2.LeanScale(Vector3.one * 3.5f, 0.2f).setEaseOutBack();
        Indicator2.LeanAlpha(0, 0.2f).setEaseOutSine();
        Front.transform.localScale = Vector3.one * 2;
        Back.transform.localScale = Vector3.one;
        Front.LeanScale(Vector3.zero, 0.25f).setEasePunch();
        Front.LeanAlpha(0, 0.25f).setEaseOutSine();
        Back.LeanScale(Vector3.one * 3f, 0.25f).setEaseOutSine();
        Indicator1.LeanScale(Vector3.zero, 0.25f).setEasePunch();
        Indicator1.LeanAlpha(0, 0.25f).setEaseOutSine();

        
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(gameObject, 0.2f);
    }

    public void End()
    {
        Front.LeanScale(Vector3.one * 2, 0);
        Front.LeanScale(Vector3.one, 0.1f);
        Indicator2.LeanScale(Vector3.one * 2.75f, 0.2f).setEaseOutBack();
        Indicator2.LeanAlpha(0, 0.2f).setEaseOutSine();

        
        Indicator1.LeanScale(Vector3.one * 1.1f, 0.25f).setEasePunch();
        Indicator1.LeanScale(Vector3.zero, 0.4f).setEaseInOutBounce();
        Indicator1.LeanColor(Color.red, 0.1f).setEaseInBounce().setOnComplete(() => {
            Back.LeanScale(Vector3.zero, 0.15f).setEaseInBack();
            Front.LeanScale(Vector3.zero, 0.1f).setEaseInBack();

            
        });

    }
}
