using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Camera camera;

    private Animator animator;

    private void Awake()
    {
        instance = this;
    }

    public void Shake()
    {
        animator.SetTrigger("Shake");
    }

    
}
