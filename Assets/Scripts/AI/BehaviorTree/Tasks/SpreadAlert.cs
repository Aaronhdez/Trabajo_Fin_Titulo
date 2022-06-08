using Mechanics;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class SpreadAlert : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private AlertController alertController;
        private EnemyController_BT enemyController;

        public SpreadAlert() {
        }

        public SpreadAlert(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            alertController = target.GetComponent<AlertController>();
            enemyController = agent.GetComponent<EnemyController_BT>();
            navMeshAgent.updateRotation = false;
        }

        public override NodeState Evaluate() {
            enemyController.HasAlreadyAlerted = true;
            alertController.UpdatePositions();
            state = NodeState.RUNNING;
            return state;
        }
    }
}