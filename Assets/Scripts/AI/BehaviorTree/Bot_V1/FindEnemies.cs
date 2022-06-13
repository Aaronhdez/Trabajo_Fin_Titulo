using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    internal class FindEnemies : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;
        [SerializeField] private int resetDestinationTime = 5;
        private float currentTime = 0;
        private float wanderSpeed;
        private float walkRadius = 50f;

        public FindEnemies() { }

        public FindEnemies(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
            wanderSpeed = agent.GetComponent<BotController>().WanderSpeed;
            SetNewDestination();
        }

        public override NodeState Evaluate() {
            navMeshAgent.speed = wanderSpeed;
            if (Time.time - currentTime > resetDestinationTime) {
                currentTime = Time.time;
                SetNewDestination();
            } else {
                if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                    character.Move(navMeshAgent.desiredVelocity, false, false);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }

        private void SetNewDestination() {
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += agent.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;
            navMeshAgent.SetDestination(finalPosition);
        }
    }
}