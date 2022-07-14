using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    internal class FindEnemies : Node {
        private GameObject agent;
        private GameObject target;
        private Animator animator;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float rotationSpeed = 7f;
        [SerializeField] private float runSpeed = 7f;
        [SerializeField] private ThirdPersonCharacter character;
        [SerializeField] private int resetDestinationTime = 5;
        private float currentTime = 0;
        private float wanderSpeed;
        private GameObject[] defaultAlertPoints;
        private GameObject[] alertPoints;
        private float walkRadius = 50f;

        public FindEnemies() { }

        public FindEnemies(GameObject agent) {
            this.agent = agent;
            target = GameObject.FindGameObjectWithTag("Player");
            animator = agent.GetComponent<Animator>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
            navMeshAgent.updateRotation = false;
            wanderSpeed = agent.GetComponent<BotController>().WanderSpeed;
            defaultAlertPoints = GameObject.FindGameObjectsWithTag("AlertPoint");
        }

        public override NodeState Evaluate() {
            navMeshAgent.speed = wanderSpeed;
            if (GetData("destination") == null) {
                SetNewRandomDestination();
            } else {
                character.Move(navMeshAgent.desiredVelocity, false, false);
            }
            float dist = navMeshAgent.remainingDistance;
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending) {
                SetNewRandomDestination();
            }
            character.Move(navMeshAgent.desiredVelocity, false, false);


            state = NodeState.RUNNING;
            return state;
        }

        private void SetNewRandomDestination() {
            alertPoints = GameObject.FindGameObjectsWithTag("AlertPoint");
            if (GetData("lastAlertPoint") != null) {
                ClearOldDestination();
            }
            var index = Random.Range(0, alertPoints.Length);
            var nextDestination = alertPoints[index];
            Debug.Log(index);
            SetData("lastAlertPoint", nextDestination);
            SetData("destination", nextDestination.transform.position);
            navMeshAgent.SetDestination(nextDestination.transform.position);
        }

        private void ClearOldDestination() {
            if (alertPoints.Length < 2) {
                EnableAllAlertPoints();
            }
            GameObject lastAlertPoint = (GameObject)GetData("lastAlertPoint");
            lastAlertPoint.SetActive(false);
            ClearData("lastAlertPoint");
            ClearData("destination");
        }

        private void EnableAllAlertPoints() {
            foreach (GameObject point in defaultAlertPoints) {
                point.SetActive(true);
            }
        }

        private void SetNewDestination() {
            var layermask = 1 << 16;
            var nodes = Physics.OverlapSphere(agent.transform.position, 50f, layermask);
            var listNodes = new List<Collider>(nodes);
            var orderedListNodes = listNodes.OrderByDescending(
                s => Vector3.Distance(s.transform.position, agent.transform.position));
            var finalPosition = orderedListNodes.First().transform.position;
            ClearData("destination");
            SetData("destination", finalPosition);
            navMeshAgent.SetDestination(finalPosition);
        }
    }
}