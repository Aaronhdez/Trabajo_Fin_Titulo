using UnityEngine;

namespace BehaviorTree {
    public class CheckTargetIsInAttackRange : Node {
        private readonly GameObject agent;
        private readonly float _attackRange;

        public CheckTargetIsInAttackRange() {
        }

        public CheckTargetIsInAttackRange(GameObject agent) {
            this.agent = agent;
            _attackRange = agent.GetComponent<EnemyController_BT>().AttackRange;
        }

        public override NodeState Evaluate() {
            object target = GetData("target");
            if (target == null) {
                state = NodeState.FAILURE;
                return state;
            }
            state = CheckIfPlayerIsOnAttackRange(target);
            return state;
        }

        private NodeState CheckIfPlayerIsOnAttackRange(object player) {
            var distanceToPlayer = Vector3.Distance(agent.transform.position, 
                ((GameObject)player).transform.position);
            if (distanceToPlayer < _attackRange) {
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}