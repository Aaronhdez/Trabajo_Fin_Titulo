using UnityEngine;

namespace BehaviorTree {
    public class CheckIfBotIsDead : Node {
        private GameObject agent;
        private BotController botController;

        public CheckIfBotIsDead() {
        }

        public CheckIfBotIsDead(GameObject agent) {
            this.agent = agent; 
            botController = agent.GetComponent<BotController>();
        }

        public override NodeState Evaluate() {
            state = NodeState.FAILURE;
            return state;
        }
    }
}