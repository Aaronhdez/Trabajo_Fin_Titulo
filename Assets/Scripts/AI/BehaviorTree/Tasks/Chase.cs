using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class Chase : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private ThirdPersonCharacter character;
        private float chaseSpeed;

        public Chase() {
        }

        public Chase(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            chaseSpeed = agent.GetComponent<EnemyController_BT>().ChaseSpeed;
            navMeshAgent.updateRotation = false;
        }

        public override NodeState Evaluate() {
            navMeshAgent.speed = chaseSpeed;
            animator.Play("Z_Run_InPlace");
            target = (GameObject) GetData("target");
            ChaseTarget();
            state = NodeState.RUNNING;
            return state;
        }

        private void ChaseTarget() {
            navMeshAgent.SetDestination(target.transform.position);
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                character.Move(navMeshAgent.desiredVelocity, false, false);
            } else {
                character.Move(Vector3.zero, false, false);
            }

        }
    }
}