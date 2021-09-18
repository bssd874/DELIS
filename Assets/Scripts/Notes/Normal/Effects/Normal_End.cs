using UnityEngine;

public class Normal_End : MonoBehaviour
{
    public Transform background;
    public Transform baseinstance;

    public Transform top;
    public Transform bottom;

    private void Awake()
    {
        background.LeanScale(Vector3.one, 0);
        baseinstance.LeanScale(Vector3.one, 0);
    }

    private void Start()
    {
        AnimateNote();
    }

    public void AnimateNote()
    {
        background.LeanScale(Vector3.zero, 0.1f).setEaseInBack();
        baseinstance.LeanScale(Vector3.zero, 0.25f).setEaseInBack();
        LeanTween.delayedCall(0, DeployIndicator);
    }

    public void DeployIndicator()
    {
        top.LeanMoveLocalY(0.5f, 0.25f).setEaseOutCirc();
        bottom.LeanMoveLocalY(-0.5f, 0.25f).setEaseOutCirc();
        HideIndicator();
    }

    public void HideIndicator()
    {
        top.gameObject.LeanAlpha(0, 0.05f).setEaseInCirc();
        bottom.gameObject.LeanAlpha(0, 0.05f).setEaseInCirc();
    }
}