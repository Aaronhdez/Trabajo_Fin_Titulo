using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class Dead : Node {
        private GameObject _agent;
        private EnemyController_BT enemyController;
        private Rigidbody agentRb;

        public Dead() {
        }

        public Dead(GameObject agent) {
            _agent = agent;
            enemyController = agent.GetComponent<EnemyController_BT>();
        }

        public override NodeState Evaluate() {
            enemyController.Kill = true;
            state = NodeState.RUNNING;
            return state;
        }
    }
}