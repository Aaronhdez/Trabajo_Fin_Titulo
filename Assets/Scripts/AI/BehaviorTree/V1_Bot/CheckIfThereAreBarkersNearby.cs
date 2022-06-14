using UnityEngine;

namespace BehaviorTree {
    public class CheckIfThereAreBarkersNearby : Node {
        private GameObject agent;
        private BotController botController;

        public CheckIfThereAreBarkersNearby(GameObject agent) {
            this.agent = agent;
            botController = agent.GetComponent<BotController>();
        }

        public override NodeState Evaluate() {
            var layerMask = 1 << 14;
            var enemiesAroundPlayer = Physics.OverlapSphere(agent.transform.position, botController.FovRange, layerMask);

            //If no enemies are detected
            if (enemiesAroundPlayer.Length == 0) {
                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}