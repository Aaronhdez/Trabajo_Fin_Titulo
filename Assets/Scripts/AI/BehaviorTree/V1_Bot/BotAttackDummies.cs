using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class BotAttackDummies : BotAttack {

        private int layerMask = 1 << 13;

        public BotAttackDummies(GameObject agent) : base(agent) {
            botController = agent.GetComponent<BotController>();
            playerRB = agent.GetComponent<Rigidbody>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
        }

        public override NodeState Evaluate() {
            TargetEnemy(layerMask);
            RotateTowardsTarget();
            ShotToEnemy(layerMask);

            state = NodeState.RUNNING;
            return state;
        }
    }
}