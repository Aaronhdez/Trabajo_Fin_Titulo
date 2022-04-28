using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_NavMesh {
    public class GunslingerMachineStateNM : ConcreteStateMachine {

        protected override void Initialize() {
            LoadEntities();
            LoadBehavioursDictionary();
        }

        private void LoadBehavioursDictionary() {
            currentState = State.None;
            behaviours = new Dictionary<State, MachineState>();
            MachineState idleState = new IdleStateNM(subject);
            MachineState chaseState = new ChaseStateNM(subject);
            MachineState deadState = new DeadState(subject);
            behaviours.Add(State.None, idleState);
            behaviours.Add(State.Chase, chaseState);
            behaviours.Add(State.Dead, deadState);
        }

        private void LoadEntities() {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void StateUpdate() {
            if (playerIsReachable()) {
                currentState = State.Chase;
            } else if (ThereAreWaypointsAvailable()) {
                currentState = State.Patrol;
            } else if (!AgentIsDead()) {
                currentState = State.None;
            } else {
                currentState = State.Dead;
            }
            behaviours[currentState].Enter();
        }

        private bool AgentIsDead() {
            return currentState == State.Dead;
        }

        private bool ThereAreWaypointsAvailable() {
            return GameObject.FindGameObjectsWithTag("PatrolPoint").Length > 0;
        }

        private bool playerIsReachable() {
            return Vector3.Distance(subject.transform.position, target.position) < 10;
        }

        protected override void StateFixedUpdate() {

        }
    }
}