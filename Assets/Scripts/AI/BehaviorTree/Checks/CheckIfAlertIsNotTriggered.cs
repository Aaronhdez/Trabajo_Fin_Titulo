using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAlertIsNotTriggered : Node {
        private readonly GameObject agent;
        private readonly float _attackRange;

        public CheckIfAlertIsNotTriggered() {
        }

        public CheckIfAlertIsNotTriggered(GameObject agent) {
            this.agent = agent;
            _attackRange = agent.GetComponent<EnemyController_BT>().attackRange;
        }

        public override NodeState Evaluate() {
            object target = GetData("target");
            if (target == null) {
                state = NodeState.FAILURE;
                return state;
            }
            state = CheckIfPlayerIsOnAlertRange(target);
            return state;
        }

        private NodeState CheckIfPlayerIsOnAlertRange(object target) {
            return NodeState.FAILURE;
        }
    }
}