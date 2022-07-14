using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class Chase : Node {
        private readonly GameObject agent;
        private GameObject target;
        private readonly Animator animator;
        private readonly NavMeshAgent navMeshAgent;
        private readonly ThirdPersonCharacter character;
        private readonly float chaseSpeed;

        public Chase() {
        }

        public Chase(GameObject agent) {
            this.agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            chaseSpeed = agent.GetComponent<EnemyController>().ChaseSpeed;
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
            ClearData("nextSlot");
            navMeshAgent.SetDestination(target.transform.position);
            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance) {
                character.Move(navMeshAgent.desiredVelocity, false, false);
            } else {
                character.Move(Vector3.zero, false, false);
            }

        }
    }
}