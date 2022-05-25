using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_BT : MonoBehaviour, IEnemyController {
    [Header("Enemy Properties")]
    [SerializeField] private float wanderSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] public int health;
    [SerializeField] private bool isDead;
    [SerializeField] private float fovRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float minimumDistanceToTarget;

    [Header("Barker Properties")]
    [SerializeField] private float maxDistanceToBeAlerted;
    [SerializeField] private float distanceToSpreadAlert;
    [SerializeField] private float timeToRespawnAlert;


    [Header("Game Instances")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundController soundController;
    public ParticleSystem deadEffect;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool mustBeKilled;
    private bool hasAlreadyAlerted;

    public bool IsDead { 
        get => isDead; set => isDead = value; }
    public bool Kill { 
        get => mustBeKilled; set => mustBeKilled = value; }
    public float ChaseSpeed { 
        get => chaseSpeed; set => chaseSpeed = value;}
    public float WanderSpeed { 
        get => wanderSpeed; set => wanderSpeed = value; }
    public float AttackSpeed { 
        get => attackSpeed; set => attackSpeed = value; }
    public float AttackRange { 
        get => attackRange; set => attackRange = value; }
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float FovRange { 
        get => fovRange; set => fovRange = value; }
    public float MinimumDistanceToTarget { 
        get => minimumDistanceToTarget; 
        set => minimumDistanceToTarget = value; }
    public float MaxDistanceToBeAlerted { 
        get => maxDistanceToBeAlerted; 
        set => maxDistanceToBeAlerted = value; }
    public float DistanceToSpreadAlert { 
        get => distanceToSpreadAlert; 
        set => distanceToSpreadAlert = value; }
    public bool HasAlreadyAlerted { get => hasAlreadyAlerted; set => hasAlreadyAlerted = value; }


    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        IsDead = false;
        Kill = false;
    }

    public void Update() {
        if (mustBeKilled) {
            StartCoroutine(PlayDeadSequence());
        }
        if (HasAlreadyAlerted) {
            StartCoroutine(PlayAlertSequence());
        }
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

    private IEnumerator PlayDeadSequence() {
        animator.Play("Z_FallingBack");
        navMeshAgent.speed = 0f;
        yield return new WaitForSecondsRealtime(3f);
        gameObject.SetActive(false);
    }

    private IEnumerator PlayAlertSequence() {
        navMeshAgent.speed = 0f;
        animator.Play("Z_Attack");
        yield return new WaitForSecondsRealtime(timeToRespawnAlert);
        HasAlreadyAlerted = false;
    }

}
