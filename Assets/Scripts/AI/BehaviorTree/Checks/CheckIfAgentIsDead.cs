using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAgentIsDead : Node {
        private readonly GameObject agent;
        private EnemyController_BT enemyController;
        private readonly float _attackRange;

        public CheckIfAgentIsDead() {
        }

        public CheckIfAgentIsDead(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            state = NodeState.FAILURE;
            return state;
        }
    }
}