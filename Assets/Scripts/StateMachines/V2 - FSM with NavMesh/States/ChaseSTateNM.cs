using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseStateNM : MachineState {
    private GameObject agent;
    private GameObject target;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float runSpeed = 7f;

    public ChaseStateNM(GameObject agent) {
        this.agent = agent;
        target = GameObject.FindGameObjectWithTag("Player");
        animator = agent.GetComponent<Animator>();
        navMeshAgent = agent.GetComponent<NavMeshAgent>();
        animator.SetFloat("VelX", 0.5f);
        animator.SetFloat("VelY", 0.5f);
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
