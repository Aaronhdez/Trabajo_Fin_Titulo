using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class Attack : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        private Rigidbody agentRb;
        private GameObject _lastTarget;
        private ThirdPersonCharacter character;
        private IPlayerController _playerController;
        private NavMeshAgent navMeshAgent;
        private float _attackCounter = 0f;
        private float _attackTime = 1f;
        private float attackRange;
        private float attackDamage;

        public Attack() {
        }

        public Attack(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = agent.GetComponent<EnemyController>().AttackSpeed;
            attackRange = agent.GetComponent<EnemyController>().AttackRange;
            attackDamage = agent.GetComponent<EnemyController>().AttackDamage;
        }

        public override NodeState Evaluate() {
            target = (GameObject)GetData("target");
            if (target != _lastTarget) {
                _lastTarget = target;
                _playerController = target.GetComponent<IPlayerController>();
            }

            _attackCounter += Time.deltaTime;
            if (_attackCounter >= _attackTime) {
                if (PlayerIsDead()) {
                    ClearData("target");
                } else {
                    AttackToPlayer();
                    _attackCounter = 0;
                }
            }
            state = NodeState.RUNNING;
            return state;
        }
        private void AttackToPlayer() {
            animator.Play("Z_Attack");
            if (Math.Abs(Vector3.Distance(agent.transform.position, target.transform.position)) <= attackRange) {
                _playerController.ApplyDamage((int) attackDamage);
            }
        }

        private bool PlayerIsDead() {
            return false;
        }
    }
}