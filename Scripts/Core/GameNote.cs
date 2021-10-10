using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNote : MonoBehaviour
{
    public NoteState state = NoteState.Miss;
    public Animator animator;

    public void Hit()
    {
        animator.Play("Hit");
        ValidateState();
        Debug.Log(GameplayData.Stats.score);
    }

    public void ValidateState()
    {
        ComboSystem.ValidateCombo(state);
    }

    public void End()
    {
        Destroy(gameObject);
    }

}

public enum NoteState {Perfect, Good, Bad, Miss}