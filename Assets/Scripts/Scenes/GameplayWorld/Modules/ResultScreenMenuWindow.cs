using UnityEngine;

public class ResultScreenMenuWindow : MonoBehaviour
{
    public RectTransform[] buttons;

    public void AnimateButtons()
    {
        foreach (RectTransform rectTransform in buttons)
        {
            rectTransform.LeanScale(Vector3.zero, 0);
            rectTransform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
        }
    }
}