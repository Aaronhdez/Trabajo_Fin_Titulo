using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MachineState {
    private GameObject agent;
    private GameObject target;
    private Animator animator;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float runSpeed = 7f;

    public ChaseState(GameObject agent) {
        this.agent = agent;
        target = GameObject.FindGameObjectWithTag("Player");
        animator = agent.GetComponent<Animator>();
        animator.SetFloat("VelX", 2);
        animator.SetFloat("VelY", 2);
    }

    public void Enter() {
        ChaseTarget();
    }

    private void ChaseTarget() {
        Quaternion targetRotation = 
            Quaternion.LookRotation(target.transform.position - agent.transform.position);
        agent.transform.rotation = 
            Quaternion.Slerp(agent.transform.rotation, targetRotation,
            Time.deltaTime * rotationSpeed);
        agent.transform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
    }

    public void Exit() {
        return;
    }
}
