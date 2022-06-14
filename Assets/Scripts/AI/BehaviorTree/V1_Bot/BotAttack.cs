using UnityEngine;

namespace BehaviorTree {
    internal class BotAttack : Node {
        private GameObject agent;

        public BotAttack(GameObject agent) {
            this.agent = agent;
        }
    }
}