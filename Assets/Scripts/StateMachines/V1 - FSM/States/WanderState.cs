using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderStateNM : MachineState {

    [SerializeField] private Animator animator;
    [SerializeField] private Transform agentTransform;
    [SerializeField] private Rigidbody enemyRB;
    [SerializeField] private GameObject nextWaypoint;
    [SerializeField] private List<GameObject> waypointsList;
    [SerializeField] private int currentIndex = -1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float runSpeed = 1f;

    public WanderStateNM(GameObject agent) {
        animator = agent.GetComponent<Animator>();
        agentTransform = agent.transform;
        enemyRB = agent.GetComponent<Rigidbody>();
    }

    public void Enter() {
    }

    public void Exit() {
    }
}
