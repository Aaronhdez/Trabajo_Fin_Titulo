using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class BotAttackBarkers : BotAttack {

        private int layerMask = 1 << 14;

        public BotAttackBarkers(GameObject agent) : base(agent) {
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