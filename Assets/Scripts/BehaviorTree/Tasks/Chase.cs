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
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;

        public Chase(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
        }

        public override NodeState Evaluate() {
            animator.Play("Z_Run_InPlace");
            target = (GameObject) GetData("target");
            ChaseTarget();
            state = NodeState.RUNNING;
            return state;
        }

        private void ChaseTarget() {
            navMeshAgent.speed = 5f;
            navMeshAgent.SetDestination(target.transform.position);
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                character.Move(navMeshAgent.desiredVelocity, false, false);
            } else {
                character.Move(Vector3.zero, false, false);
            }

        }
    }
}