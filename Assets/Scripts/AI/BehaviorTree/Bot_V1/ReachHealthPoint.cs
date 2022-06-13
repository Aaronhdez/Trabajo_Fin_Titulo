using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    internal class ReachHealthPoint : Node {
        private GameObject agent;
        private NavMeshAgent navMeshAgent;
        private BotController botController;

        public ReachHealthPoint(GameObject agent) {
            this.agent = agent;
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
        }

        public override NodeState Evaluate() {
            //Si la distancia al prop es pequeña, restaurar la salud.
            state = NodeState.RUNNING;
            return state;
        }
    }
}