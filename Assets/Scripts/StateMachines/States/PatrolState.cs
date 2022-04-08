using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MachineState {
    [SerializeField] private Animator animator;
    [SerializeField] private Transform agentTransform;
    [SerializeField] private Rigidbody enemyRB;
    [SerializeField] private GameObject nextWaypoint;
    [SerializeField] private List<GameObject> waypointsList;
    [SerializeField] private int currentIndex = -1;
    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private float runSpeed = 3f;

    public PatrolState(GameObject agent) {
        animator = agent.GetComponent<Animator>();
        agentTransform = agent.transform;
        enemyRB = agent.GetComponent<Rigidbody>(); 
    }

    public void Enter() {
        if (currentIndex == -1) {
            nextWaypoint = GetRandomWaypoint();
            if (nextWaypoint == null) return;
            waypointsList = nextWaypoint.GetComponentInParent<PatrolPath>().Waypoints;
            currentIndex = FindWaypointIndex(waypointsList);
        }
        if (Vector3.Distance(agentTransform.position, nextWaypoint.transform.position) < 0.5f) {
            currentIndex = (currentIndex + 1) % waypointsList.Count;
            nextWaypoint = waypointsList[currentIndex]; ;
        }
        MoveToPoint();
    }
    private void MoveToPoint() {
        var currentPosition = agentTransform.position; 
        Quaternion targetRotation = Quaternion.LookRotation(nextWaypoint.transform.position - agentTransform.position);
        agentTransform.rotation = Quaternion.Slerp(agentTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        agentTransform.Translate(Vector3.forward * Time.deltaTime * runSpeed);
        SetAnimatorProperties(currentPosition);
    }

    private void SetAnimatorProperties(Vector3 currentPosition) {
        var positionVariance = Vector3.Normalize((agentTransform.position - currentPosition));
        animator.SetFloat("VelX", positionVariance.z);
        animator.SetFloat("VelY", positionVariance.x);
    }

    private int FindWaypointIndex(List<GameObject> waypointsList) {
        for (int i = 0; i < waypointsList.Count; i++) {
            if (waypointsList[i].Equals(nextWaypoint)) {
                return i;
            }
        }
        return -1;
    }

    private GameObject GetRandomWaypoint() {
        var wayPointsAvailable = GameObject.FindGameObjectsWithTag("PatrolPoint");
        var randomIndex = Random.Range(0, wayPointsAvailable.Length - 1);
        return wayPointsAvailable[randomIndex];
    }



    public void Exit() {
        return;
    }
}
