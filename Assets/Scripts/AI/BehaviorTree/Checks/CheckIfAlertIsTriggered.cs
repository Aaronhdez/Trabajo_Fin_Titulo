using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAlertIsTriggered : Node {
        private readonly GameObject agent;
        private readonly float _attackRange;

        public CheckIfAlertIsTriggered() {
        }

        public CheckIfAlertIsTriggered(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            if (AlertManager.AlertHasBeenTriggered()) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}