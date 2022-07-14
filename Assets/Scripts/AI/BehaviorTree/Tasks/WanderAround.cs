using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class WanderAround : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;
        [SerializeField] private int resetDestinationTime;
        private float currentTime = 0;
        private float wanderSpeed;

        public WanderAround() {
        }

        public WanderAround(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
            wanderSpeed = agent.GetComponent<EnemyController>().WanderSpeed;
            SetNewDestination();
        }

        public override NodeState Evaluate() {
            navMeshAgent.speed = wanderSpeed;
            animator.Play("Z_Walk_InPlace");
            if (Time.time - currentTime > resetDestinationTime) {
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
            resetDestinationTime = Random.Range(5, 16);
            currentTime = Time.time;
            var newX = Random.Range(-50, 50) + navMeshAgent.transform.position.x;
            var newZ = Random.Range(-50, 50) + navMeshAgent.transform.position.z;
            var newPosition = new Vector3(newX, agent.transform.position.y, newZ);
            navMeshAgent.SetDestination(newPosition);
        }

    }
}