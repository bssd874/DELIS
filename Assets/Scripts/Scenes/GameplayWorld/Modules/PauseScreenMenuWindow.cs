using UnityEngine;

public class PauseScreenMenuWindow : MonoBehaviour
{
    public RectTransform[] pauseMenuButtonLists;

    public void AnimateOpenMenu()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        transform.gameObject.LeanCancel();
        canvasGroup.alpha = 0;

        foreach (RectTransform rectTransform in pauseMenuButtonLists)
        {
            rectTransform.gameObject.LeanCancel();
        }
        canvasGroup.LeanAlpha(1, 0.25f).setEaseOutExpo().setIgnoreTimeScale(true);
        transform.gameObject.LeanScale(Vector3.one, 0.5f).setEaseOutBack().setIgnoreTimeScale(true);

        int i = 0;
        foreach (RectTransform rectTransform in pauseMenuButtonLists)
        {
            i++;
            rectTransform.gameObject.LeanScale(Vector3.one, 0.25f).setDelay(i * 0.05f).setEaseOutBack().setIgnoreTimeScale(true);
        }
    }

    public void AnimateCloseMenu()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        transform.gameObject.LeanCancel();
        canvasGroup.alpha = 1;

        foreach (RectTransform rectTransform in pauseMenuButtonLists)
        {
            rectTransform.gameObject.LeanCancel();
        }
        canvasGroup.LeanAlpha(0, 0.5f).setEaseInExpo().setIgnoreTimeScale(true);
        transform.gameObject.LeanScale(Vector3.one * 0.8f, 0.5f).setEaseInBack().setIgnoreTimeScale(true);

        int i = 0;
        foreach (RectTransform rectTransform in pauseMenuButtonLists)
        {
            i++;
            rectTransform.gameObject.LeanScale(Vector3.zero, 0.25f).setDelay(i * 0.05f).setEaseInBack().setIgnoreTimeScale(true);
        }
    }
}