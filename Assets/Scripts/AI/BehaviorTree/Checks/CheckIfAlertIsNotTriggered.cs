using Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAlertIsNotTriggered : Node {
        private readonly GameObject agent;
        private AlertController alertController;
        private readonly float _attackRange;

        public CheckIfAlertIsNotTriggered() {
        }

        public CheckIfAlertIsNotTriggered(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            if (!AlertManager.AlertHasBeenTriggered()) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;            
        }
    }
}