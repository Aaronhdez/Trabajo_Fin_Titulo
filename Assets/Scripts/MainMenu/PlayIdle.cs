using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIdle : MonoBehaviour
{

    [SerializeField] Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("ArmaCogida", true);
    }

    void Update()
    {
        animator.SetFloat("VelX", 0f);
        animator.SetFloat("VelX", 0f);
    }
}
