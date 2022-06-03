using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckTargetIsInFOVRange : Node {
        private readonly GameObject agent;
        private readonly float _fovRange;

        public CheckTargetIsInFOVRange() {
        }

        public CheckTargetIsInFOVRange(GameObject agent) {
            this.agent = agent;
            _fovRange = agent.GetComponent<EnemyController_BT>().FovRange;
        }

        public override NodeState Evaluate() {
            object target = GetData("target");
            if (target == null) {
                state = CheckIfPlayerIsOnFOVRange();
                return state;
            }
            state = NodeState.SUCCESS;
            return state;
        }

        private NodeState CheckIfPlayerIsOnFOVRange() {
            var player = GameObject.Find("PlayerPrefab");

            var distanceToPlayer = Vector3.Distance(
                agent.transform.position, player.transform.position);
            
            if (Math.Abs(distanceToPlayer) < _fovRange) {
                _parent._parent.SetData("target", player);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}