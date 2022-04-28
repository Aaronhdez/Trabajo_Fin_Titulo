using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_NavMesh {
    public class GunslingerMachineStateNM : ConcreteStateMachine {

        private EnemyController enemyController;

        protected override void Initialize() {
            LoadEntities();
            LoadBehavioursDictionary();
        }

        private void LoadEntities() {
            enemyController = GetComponent<EnemyController>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LoadBehavioursDictionary() {
            currentState = State.Wander;
            behaviours = new Dictionary<State, MachineState>();
            MachineState wanderState = new WanderStateNM(subject);
            MachineState chaseState = new ChaseStateNM(subject);
            MachineState deadState = new DeadState(subject);
            behaviours.Add(State.Wander, wanderState);
            behaviours.Add(State.Chase, chaseState);
            behaviours.Add(State.Dead, deadState);
        }

        protected override void StateUpdate() {
            if (PlayerIsReachable()) {
                currentState = State.Chase;
            } else if (!AgentIsDead()) {
                currentState = State.Wander;
            }
            if (AgentIsDead()) {
                currentState = State.Dead;
                Destroy(subject);
            }
            behaviours[currentState].Enter();
        }

        private bool AgentIsDead() {
            return enemyController.IsDead;
        }

        private bool PlayerIsReachable() {
            return Vector3.Distance(subject.transform.position, target.position) < 10;
        }

        protected override void StateFixedUpdate() {

        }
    }
}