using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTree {
    public class BotAttack : Node {
        protected GameObject agent;
        protected BotController botController;
        protected Rigidbody playerRB;
        protected NavMeshAgent navMeshAgent;
        private Vector3 _dir;

        public BotAttack(GameObject agent) {
            this.agent = agent;
        }

        protected void TargetEnemy(int layerMask) {
            var enemiesAroundPlayer = Physics.OverlapSphere(agent.transform.position, botController.FovRange, layerMask);
            var enemiesList = new List<Collider>(enemiesAroundPlayer);
            var orderedList = enemiesList.OrderByDescending(
                e => Vector3.Distance(agent.transform.position, e.transform.position));
            ClearData("target");
            SetData("target", orderedList.First().gameObject);
        }

        protected void RotateTowardsTarget() {
            var target = (GameObject)GetData("target");
            _dir = target.transform.position - playerRB.position;
            _dir.Normalize();
            agent.transform.rotation = Quaternion.Slerp(
                agent.transform.rotation, Quaternion.LookRotation(_dir), 10f * Time.deltaTime);
        }

        protected void RayCastShot() {
            Debug.Log("Shot");
        }
    }
}