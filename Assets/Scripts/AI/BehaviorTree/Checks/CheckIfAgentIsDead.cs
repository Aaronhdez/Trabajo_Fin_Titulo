using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAgentIsDead : Node {
        private readonly GameObject agent;
        private EnemyController enemyController;

        public CheckIfAgentIsDead() {
        }

        public CheckIfAgentIsDead(GameObject agent) {
            this.agent = agent;
            enemyController = agent.GetComponent<EnemyController>();
        }

        public override NodeState Evaluate() {
            if (enemyController.IsDead) {
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}