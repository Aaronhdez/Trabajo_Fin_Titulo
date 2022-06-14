using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace BehaviorTree {
    internal class ReachHealthPoint : Node {
        private GameObject agent;
        private NavMeshAgent navMeshAgent;
        private ThirdPersonCharacter character;
        private BotController botController;
        private Vector3 nearestSupplyBox;

        public ReachHealthPoint(GameObject agent) {
            this.agent = agent;
            botController = agent.GetComponent<BotController>();
            navMeshAgent = agent.GetComponent<NavMeshAgent>();
            character = agent.GetComponent<ThirdPersonCharacter>();
        }

        public override NodeState Evaluate() {
            if (GetData("healthPoint") == null) {
                nearestSupplyBox = GetNearestHealthPoint();
                SetData("healthPoint", nearestSupplyBox);
            } else {
                nearestSupplyBox = (Vector3)GetData("healthPoint");
            }

            if (AgentIsNearFromSupplyBox()) {
                botController.RestoreHealth();
                ClearData("healthPoint");
                if (GetData("destination") != null) {
                    navMeshAgent.SetDestination((Vector3)GetData("destination"));
                } else {
                    
                }
            } else {
                navMeshAgent.speed = botController.ChaseSpeed;
                navMeshAgent.SetDestination(nearestSupplyBox);
                character.Move(navMeshAgent.desiredVelocity, false, false);
            }
            state = NodeState.RUNNING;
            return state;
        }

        private bool AgentIsNearFromSupplyBox() {
            return Vector3.Distance(agent.transform.position, nearestSupplyBox) <= 2f;
        }

        private Vector3 GetNearestHealthPoint() {
            var layermask = 1 << 15;
            var supplyBoxes = Physics.OverlapSphere(
                agent.transform.position, 10f, layermask);

            var boxesList = new List<Collider>(supplyBoxes);
            var orderedBosexList = boxesList.OrderBy(
                s => Vector3.Distance(s.transform.position, agent.transform.position));

            Debug.Log(orderedBosexList.First());

            return orderedBosexList.First().transform.position;
        }
    }
}