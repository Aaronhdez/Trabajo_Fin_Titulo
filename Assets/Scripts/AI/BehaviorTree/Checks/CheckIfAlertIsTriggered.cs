using Mechanics;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAlertIsTriggered : Node {
        private readonly GameObject agent;

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