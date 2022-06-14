using UnityEngine;

namespace BehaviorTree {
    public class CheckIfThereAreBarkersNearby : Node {
        private GameObject agent;

        public CheckIfThereAreBarkersNearby(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            state = NodeState.FAILURE;
            return state;
        }
    }
}