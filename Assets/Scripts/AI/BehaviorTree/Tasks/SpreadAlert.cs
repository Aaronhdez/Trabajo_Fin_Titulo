using Mechanics;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class SpreadAlert : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;
        private AlertController alertController;

        public SpreadAlert() {
        }

        public SpreadAlert(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            alertController = target.GetComponent<AlertController>();
            navMeshAgent.updateRotation = false;
            navMeshAgent.speed = 0f;
        }

        public override NodeState Evaluate() {
            //alertController.UpdatePositions();
            animator.Play("Z_Attack");
            state = NodeState.RUNNING;
            return state;
        }
    }
}