using UnityEngine;
using UnityEngine.SceneManagement;

namespace BehaviorTree {
    internal class BotDead : Node {
        private GameObject agent;

        public BotDead() {
        }

        public BotDead(GameObject agent) {
            this.agent = agent;
        }
        public override NodeState Evaluate() {
            SceneManager.LoadScene("TestingScene");
            state = NodeState.RUNNING;
            return state;
        }
    }
}