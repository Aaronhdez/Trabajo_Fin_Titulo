using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAgentCanSpreadAlert : Node {
        private readonly GameObject agent;
        private readonly float distanceToSpreadAlert;

        public CheckIfAgentCanSpreadAlert() {
        }

        public CheckIfAgentCanSpreadAlert(GameObject agent) {
            this.agent = agent;
            distanceToSpreadAlert = agent.GetComponent<EnemyController_BT>().DistanceToSpreadAlert;
        }

        public override NodeState Evaluate() {
            var lastAlertPosition = AlertManager.GetLastAlertPosition();
            if (Vector3.Distance(agent.transform.position, lastAlertPosition) < distanceToSpreadAlert) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

    }
}