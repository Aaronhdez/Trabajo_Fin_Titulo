using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    public class Dead : Node {
        private GameObject _agent;
        private GameObject target;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Rigidbody agentRb;

        public Dead() {
        }

        public Dead(GameObject agent) {
            _agent = agent;
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
        }

        public override NodeState Evaluate() {
            PlayDeadSequence();
            state = NodeState.RUNNING;
            return state;
        }

        private IEnumerator PlayDeadSequence() {
            navMeshAgent.speed = 0f;
            agentRb.AddForce(Vector3.right * 5, ForceMode.Impulse);
            animator.Play("Z_FallingBack");
            yield return new WaitForSeconds(5f);
        }
    }
}