using UnityEngine;
using UnityEngine.SceneManagement;

namespace BehaviorTree {
    internal class BotDead : Node {
        private GameObject agent;
        private BotController botController;

        public BotDead() {
        }

        public BotDead(GameObject agent) {
            this.agent = agent;
            botController = agent.GetComponent<BotController>();
        }
        public override NodeState Evaluate() {
            botController.IsDead = true;
            state = NodeState.RUNNING;
            return state;
        }
    }
}