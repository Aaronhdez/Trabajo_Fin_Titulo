using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FiniteStateMachine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Properties")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] public int health;
    [SerializeField] private bool isDead;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private State defaultState = State.Wander;
    [SerializeField] private SoundController soundController;
    public ParticleSystem deadEffect;

    public float ChaseSpeed { get => chaseSpeed; set => chaseSpeed = value; }
    public float WanderSpeed { get => wanderSpeed; set => wanderSpeed = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        IsDead = false;
    }

    public void ApplyDamage(int damagedReceived) {
        health -= (health - damagedReceived > 0) ? damagedReceived : health;
        CheckHealthStatus();
    }

    private void CheckHealthStatus() {
        if (health == 0) {
            Instantiate(deadEffect, transform.position, Quaternion.LookRotation(Vector3.up));
            isDead = true;
        }
    }
}
