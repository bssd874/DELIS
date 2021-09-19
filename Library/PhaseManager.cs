using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager main;

    void Awake()
    {
        main = this;
    }
}