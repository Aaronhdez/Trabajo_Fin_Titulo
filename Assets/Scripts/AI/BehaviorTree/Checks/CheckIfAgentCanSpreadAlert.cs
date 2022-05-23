using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAgentCanSpreadAlert : Node {
        private readonly GameObject agent;
        private readonly float _attackRange;

        public CheckIfAgentCanSpreadAlert() {
        }

        public CheckIfAgentCanSpreadAlert(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            var lastAlertPosition = AlertManager.GetLastAlertPosition();
            if (Vector3.Distance(agent.transform.position, lastAlertPosition) < 20f) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

    }
}