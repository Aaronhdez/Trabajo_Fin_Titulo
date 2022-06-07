using Mechanics;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class ChaseOnAlert : Node {
        private GameObject agent;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private ThirdPersonCharacter character;
        private float minimumDistanceToTarget;
        private float maxDistanceToBeAlerted;
        private float chaseSpeed;

        public ChaseOnAlert() {
        }

        public ChaseOnAlert(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            minimumDistanceToTarget = agent.GetComponent<EnemyController_BT>().MinimumDistanceToTarget;
            maxDistanceToBeAlerted = agent.GetComponent<EnemyController_BT>().MaxDistanceToBeAlerted;
            chaseSpeed = agent.GetComponent<EnemyController_BT>().ChaseSpeed;
            navMeshAgent.updateRotation = false;
        }

        public override NodeState Evaluate() {
            navMeshAgent.speed = chaseSpeed;
            animator.Play("Z_Run_InPlace");
            var destiny = SelectBestDestinationPoint();
            MoveToDestinationPoint(destiny);
            state = NodeState.RUNNING;
            return state;
        }

        private Vector3 SelectBestDestinationPoint() {
            //Cambio insertado para el sistema de slots
            if (GetData("nextSlot") != null) {
                return ((GameObject)GetData("nextSlot")).transform.position;
            }

            //Sin slots
            if (Vector3.Distance(AlertManager.GetLastAlertPosition(),
                agent.transform.position) <= maxDistanceToBeAlerted) {
                return AlertManager.GetLastAlertPosition();
            }
            return TakeBestPointOfZone();
        }

        private void MoveToDestinationPoint(Vector3 destiny) {
            navMeshAgent.SetDestination(destiny);
            if (navMeshAgent.remainingDistance > minimumDistanceToTarget) {
                character.Move(navMeshAgent.desiredVelocity, false, false);
            } else {
                character.Move(Vector3.zero, false, false);
            }
        }

        private Vector3 TakeBestPointOfZone() {
            var lastZoneReported = AlertManager.GetLastZoneReported()
                .OrderBy(pos => Vector3.Distance(pos, agent.transform.position));
            return lastZoneReported.First();
        }
    }
}