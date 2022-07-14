using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FiniteStateMachine;

public class EnemyController_FSM : EnemyController
{
    [SerializeField] private State defaultState = State.Wander;

    public override void ApplyDamage(int damagedReceived) {
        health -= (health - damagedReceived > 0) ? damagedReceived : health;
        CheckHealthStatus();
    }

    protected override void CheckHealthStatus() {
        if (health == 0) {
            Instantiate(deadEffect, transform.position, Quaternion.LookRotation(Vector3.up));
            isDead = true;
        }
    }
}
