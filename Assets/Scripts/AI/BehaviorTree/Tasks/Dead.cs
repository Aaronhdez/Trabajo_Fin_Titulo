using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class Dead : Node {
        private GameObject _agent;
        private GameObject target;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private EnemyController_BT enemyController;
        private Rigidbody agentRb;

        public Dead() {
        }

        public Dead(GameObject agent) {
            _agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            enemyController = agent.GetComponent<EnemyController_BT>();
        }

        public override NodeState Evaluate() {
            enemyController.Kill = true;
            state = NodeState.RUNNING;
            return state;
        }
    }
}