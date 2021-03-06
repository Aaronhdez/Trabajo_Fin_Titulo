using Mechanics;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class Alert : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        private Mechanics.AlertController alertController;
        private EnemyController enemyController;

        public Alert() {
        }

        public Alert(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            alertController = target.GetComponent<Mechanics.AlertController>();
            enemyController = agent.GetComponent<EnemyController>();
            navMeshAgent.updateRotation = false;
        }

        public override NodeState Evaluate() {
            enemyController.HasAlreadyAlerted = true;
            alertController.TriggerAlert();
            state = NodeState.RUNNING;
            return state;
        }
    }
}