using UnityEngine;
using UnityEngine.Events;

public class Note : MonoBehaviour
{
    public int normalPhase;
    public int hitPhase;
    public PhaseHandler phaseHandler;

    public UnityEvent onStart;
    public UnityEvent onHit;
    public UnityEvent onDestroy;

    public void StartNote()
    {
        onStart.Invoke();
        transform.position += new Vector3(0, 0, 1);
        transform.LeanMoveLocalZ(0, 2);
        phaseHandler.ActivatePhaseIndex(normalPhase);
    }

    public void HitNote()
    {
        onHit.Invoke();
        phaseHandler.DeactivatePhaseIndex(0);
        phaseHandler.ActivatePhaseIndex(1);
    }

    public void DestroyThis()
    {
        onDestroy.Invoke();
        Destroy(gameObject);
    }
}