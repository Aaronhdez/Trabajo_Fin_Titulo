using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAgentCanBeAlerted : Node {
        private readonly GameObject agent;
        private readonly float maxDistanceToBeAlerted;

        public CheckIfAgentCanBeAlerted() {
        }

        public CheckIfAgentCanBeAlerted(GameObject agent) {
            this.agent = agent;
            maxDistanceToBeAlerted = agent.GetComponent<EnemyController_BT>().MaxDistanceToBeAlerted;
        }

        public override NodeState Evaluate() {
            var lastAlertPosition = AlertManager.GetLastAlertPosition();
            if (Vector3.Distance(agent.transform.position, lastAlertPosition) < maxDistanceToBeAlerted) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}