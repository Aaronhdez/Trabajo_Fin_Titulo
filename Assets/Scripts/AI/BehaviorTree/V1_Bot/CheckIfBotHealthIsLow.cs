using UnityEngine;

namespace BehaviorTree {
    public class CheckIfBotHealthIsLow : Node {
        private GameObject agent;
        private BotController botController;

        public CheckIfBotHealthIsLow(GameObject agent) {
            this.agent = agent;
            botController = agent.GetComponent<BotController>();
        }

        public override NodeState Evaluate() {
            if (botController.LowHealth()) {
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}