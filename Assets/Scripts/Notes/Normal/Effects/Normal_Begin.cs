using UnityEngine;

public class Normal_Begin : MonoBehaviour
{
    public Transform background;
    public Transform baseinstance;

    public Transform top;
    public Transform bottom;

    private void Awake()
    {
        background.LeanScale(Vector3.zero, 0);
        baseinstance.LeanScale(Vector3.zero, 0);
    }

    private void Start()
    {
        AnimateNote();
    }

    public void AnimateNote()
    {
        background.LeanScale(Vector3.zero, 0);
        baseinstance.LeanScale(Vector3.zero, 0);

        background.gameObject.LeanColor(Color.green, 2).setEaseInExpo();

        background.LeanRotateZ(135, 1.5f).setEaseInOutExpo();
        background.LeanScale(Vector3.one, 1).setEaseOutBack();
        baseinstance.LeanScale(Vector3.one, 2).setEaseOutBack();
        LeanTween.delayedCall(1, DeployIndicator);
    }

    public void DeployIndicator()
    {
        top.LeanMoveLocalY(0, 1).setEaseInCirc();
        bottom.LeanMoveLocalY(0, 1).setEaseInCirc();
    }
}