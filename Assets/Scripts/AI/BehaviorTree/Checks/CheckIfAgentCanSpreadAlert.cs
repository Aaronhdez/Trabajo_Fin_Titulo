using Mechanics;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAgentCanSpreadAlert : Node {
        private readonly GameObject agent;
        private readonly float distanceToSpreadAlert;
        private readonly EnemyController enemyController;

        public CheckIfAgentCanSpreadAlert() {
        }

        public CheckIfAgentCanSpreadAlert(GameObject agent) {
            this.agent = agent;
            distanceToSpreadAlert = agent.GetComponent<EnemyController>().DistanceToSpreadAlert;
            enemyController = agent.GetComponent<EnemyController>();
        }

        public override NodeState Evaluate() {
            if (enemyController.HasAlreadyAlerted) {
                state = NodeState.FAILURE;
                return state;
            }
            var lastAlertPosition = AlertManager.GetLastAlertPosition();
            if (Vector3.Distance(agent.transform.position, lastAlertPosition) <= distanceToSpreadAlert) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

    }
}