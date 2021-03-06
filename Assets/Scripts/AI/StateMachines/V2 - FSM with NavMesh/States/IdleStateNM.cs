using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateNM : MachineState {

    private Animator animator;

    public IdleStateNM(GameObject gameObject) {
        animator = gameObject.GetComponent<Animator>();
    }

    public void Enter() {
        animator.SetFloat("VelX", 0);
        animator.SetFloat("VelY", 0);
    }

    public void Exit() {
    }
}
