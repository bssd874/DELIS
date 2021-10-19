using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNote : MonoBehaviour
{
    public GameObject stateIndicator;
    public NoteState state = NoteState.Miss;
    public Animator animator;

    public void Hit()
    {
        animator.Play("Hit");
        ValidateState();

    }

    public void ValidateState()
    {
        GameplayData.Validate(state);
        SpawnIndicator();
    }

    public void SpawnIndicator()
    {
        StateIndicator indicator = Instantiate(stateIndicator, transform.position, transform.rotation, transform.parent).GetComponent<StateIndicator>();
        indicator.Indicate(state);
    }

    public void End()
    {
        Destroy(gameObject);
    }

}