using System;
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
        private float lastTimeShot = 0f;

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
            navMeshAgent.speed = 0;
            var target = (GameObject)GetData("target");
            _dir = target.transform.position - playerRB.position;
            _dir.Normalize();
            agent.transform.rotation = Quaternion.Slerp(
                agent.transform.rotation, Quaternion.LookRotation(_dir), 10f * Time.deltaTime);
        }

        protected void ShotToEnemy(int layerMask) {
            if (Time.time - lastTimeShot >= botController.FireRate) { 
                lastTimeShot = Time.time;
                RaycastShot(layerMask);
            }
        }

        private void RaycastShot(int layerMask) {
            RaycastHit impactInfo;
            var target = (GameObject)GetData("target");
            var direction = SetRayDirection(target);
            if (Physics.Raycast(agent.transform.position, 
                direction, out impactInfo, botController.AttackRange, layerMask)) {

                Ray ray = new Ray(agent.transform.position, direction);
                Debug.DrawLine(ray.origin, impactInfo.point, Color.red, 0.5f);
                
                Debug.Log("Shot to:  ");
                target.GetComponent<EnemyController>().ApplyDamage(botController.attackDamage);
            }

            if (target.GetComponent<EnemyController>().IsDead) {
                ClearData("target");
            }
        }

        private Vector3 SetRayDirection(GameObject target) {
            var from = agent.transform.position + new Vector3(0, 1f, 0);
            var to = target.transform.position + new Vector3(0, target.transform.localScale.y / 2, 0);
            return -(from - to);
        }
    }
}