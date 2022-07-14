using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace FSM_NavMesh {
    public class WanderStateNM : MachineState {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;
        [SerializeField] private int resetDestinationTime;
        private float currentTime = 0;

        public WanderStateNM(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.speed = 1f;
            SetNewDestination();
        }

        public void Enter() {
            WanderAround();
        }

        private void WanderAround() {
            if(Time.time - currentTime > resetDestinationTime) {
                SetNewDestination();
            } else { 
                if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                    character.Move(navMeshAgent.desiredVelocity, false, false);
                }
            }
        }

        private void SetNewDestination() {
            resetDestinationTime = Random.Range(5, 16);
            currentTime = Time.time;
            var newX = Random.Range(-50, 50) + navMeshAgent.transform.position.x;
            var newZ = Random.Range(-50, 50) + navMeshAgent.transform.position.z;
            var newPosition = new Vector3(newX, agent.transform.position.y, newZ);
            navMeshAgent.SetDestination(newPosition);
        }

        public void Exit() {
            return;
        }
    }
}