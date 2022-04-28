using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class ChaseStateNM : MachineState {
    private GameObject agent;
    private GameObject target;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float rotationSpeed = 7f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private ThirdPersonCharacter character;

    public ChaseStateNM(GameObject agent) {
        this.agent = agent;
        target = GameObject.FindGameObjectWithTag("Player");
        animator = agent.GetComponent<Animator>();
        navMeshAgent = agent.GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
    }

    public void Enter() {
        ChaseTarget();
    }

    private void ChaseTarget() {
        navMeshAgent.SetDestination(target.transform.position);
        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
            character.Move(navMeshAgent.desiredVelocity, false, false);
        } else {
            character.Move(Vector3.zero, false, false);
        }

    }

    public void Exit() {
        return;
    }
}
