using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    public GameObject background;
    public Image frontground;
    public ResultScreenMenuWindow window;

    public static ResultScreen main;

    private void Awake()
    {
        main = this;
    }

    public void Display()
    {
        window.gameObject.SetActive(true);
        window.gameObject.LeanScale(Vector3.zero, 0);

        window.gameObject.LeanScale(Vector3.one * 0.5f, 1).setEaseOutBack().setOnComplete(() =>
        {
            window.gameObject.LeanRotateZ(720, 2).setEaseInOutBack();
            window.gameObject.LeanScale(Vector3.one, 1).setEaseInOutExpo().setDelay(2).setOnComplete(() =>
            {
                window.AnimateButtons();
            });
        }).setDelay(1);
    }
}