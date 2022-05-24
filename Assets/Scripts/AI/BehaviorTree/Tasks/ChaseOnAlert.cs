using Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class ChaseOnAlert : Node {
        private GameObject agent;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;

        public ChaseOnAlert() {
        }

        public ChaseOnAlert(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
        }

        public override NodeState Evaluate() {
            animator.Play("Z_Run_InPlace");
            var destiny = SelectBestDestinationPoint();
            MoveToDestinationPoint(destiny);
            state = NodeState.RUNNING;
            return state;
        }

        private void MoveToDestinationPoint(Vector3 destiny) {
            navMeshAgent.speed = 5f;
            navMeshAgent.SetDestination(destiny);
            if (navMeshAgent.remainingDistance > 5f) {
                character.Move(navMeshAgent.desiredVelocity, false, false);
            } else {
                character.Move(Vector3.zero, false, false);
            }
        }

        private Vector3 SelectBestDestinationPoint() {
            if (Vector3.Distance(AlertManager.GetLastAlertPosition(),
                agent.transform.position) <= 10f) {
                return AlertManager.GetLastAlertPosition();
            }
            return TakeBestPointOfZone();
        }

        private Vector3 TakeBestPointOfZone() {
            var lastZoneReported = AlertManager.GetLastZoneReported()
                .OrderBy(pos => Vector3.Distance(pos, agent.transform.position));
            return lastZoneReported.First();
        }
    }
}