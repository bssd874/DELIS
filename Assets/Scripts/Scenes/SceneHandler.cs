using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public RectTransform background;

    public void Start()
    {
        OpenScene();
    }

    public LTDescr OpenScene()
    {
        Time.timeScale = 1;
        background.LeanAlpha(1, 0);
        return background.LeanAlpha(0, 0.5f).setEaseOutExpo().setIgnoreTimeScale(true);
    }

    public LTDescr CloseScene()
    {
        return background.LeanAlpha(1, 0.5f).setEaseInExpo().setIgnoreTimeScale(true);
    }

    public void ChangeScene(string name)
    {
        background.LeanCancel();
        CloseScene().setOnComplete(() =>
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        });
    }
}