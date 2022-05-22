using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class Alert : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;
        [SerializeField] private int resetDestinationTime;
        private float currentTime = 0;

        public Alert() {
        }

        public Alert(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.speed = 1f;
        }

        public override NodeState Evaluate() {
            
            state = NodeState.FAILURE;
            return state;
        }
    }
}