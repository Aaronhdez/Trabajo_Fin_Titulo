using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfThereAreDummiesNearby : Node {
        private GameObject agent;
        private BotController botController;

        public CheckIfThereAreDummiesNearby(GameObject agent) {
            this.agent = agent;
            botController = agent.GetComponent<BotController>();
        }

        public override NodeState Evaluate() {
            var layerMask = 1 << 13;
            var enemiesAroundPlayer = Physics.OverlapSphere(agent.transform.position, botController.FovRange, layerMask);

            //If no enemies are detected
            if (enemiesAroundPlayer.Length == 0) {
                state = NodeState.FAILURE;
                return state;
            }
            
            state = NodeState.SUCCESS;
            return state;
        }
    }
}