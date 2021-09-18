using UnityEngine;

public class Normal_Hit : MonoBehaviour
{
    public Transform flash;
    public Transform explosion;
    public Transform debris;
    public Transform shockwave;

    private void Start()
    {
        flash.LeanScale(Vector3.one * 0.5f, 0.2f);
        flash.gameObject.LeanAlpha(0, 0.2f).setEaseInExpo();
        explosion.LeanScale(Vector3.one * 0.5f, 0.25f).setEasePunch();
        debris.LeanScale(Vector3.one * 4, 0.15f).setEaseOutExpo();
        debris.gameObject.LeanAlpha(0, 0.15f).setEaseOutExpo();
        explosion.gameObject.LeanAlpha(0, 0.25f);
        shockwave.LeanScale(Vector3.one * 0.5f, 0.25f).setEaseOutExpo();
        shockwave.gameObject.LeanAlpha(0, 0.5f).setEaseOutExpo();
    }
}