using System;
using System.Collections;
using System.Collections.Generic;
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
        private PlayerController _playerController;
        private NavMeshAgent navMeshAgent;
        private float _attackCounter = 0f;
        private float _attackTime = 1f;

        public Attack() {
        }

        public Attack(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            navMeshAgent.speed = 4f;
        }

        public override NodeState Evaluate() {
            target = (GameObject)GetData("target");
            if (target != _lastTarget) {
                _lastTarget = target;
                _playerController = target.GetComponent<PlayerController>();
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
            if (Math.Abs(Vector3.Distance(agent.transform.position, target.transform.position)) < 6f) {
                _playerController.ApplyDamage(30);
            }
        }

        private IEnumerable Animate() {
            animator.Play("Z_Attack");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length
                + animator.GetCurrentAnimatorStateInfo(0).speed);
        }

        private bool PlayerIsDead() {
            return false;
        }
    }
}