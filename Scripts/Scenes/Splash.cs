using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    // Core
    public Animator animator;

    // Default

    void Start()
    {
        Initialize();
    }

    // Main

    public void Initialize()
    {
        animator.Play("Begin");
    }

    // Function


}
