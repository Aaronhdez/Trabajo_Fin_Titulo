using Mechanics;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAlertIsNotTriggered : Node {
        private readonly GameObject agent;

        public CheckIfAlertIsNotTriggered() {
        }

        public CheckIfAlertIsNotTriggered(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            if (!AlertManager.AlertHasBeenTriggered()) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;            
        }
    }
}