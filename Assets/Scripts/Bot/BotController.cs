using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    [Header("General Properties")]
    [SerializeField] protected float wanderSpeed;
    [SerializeField] protected float chaseSpeed;
    public int health;
    public int attackDamage;
    [SerializeField] protected bool isDead;
    [SerializeField] protected float fovRange;
    [SerializeField] protected float attackRange;

    [Header("Game Instances")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected ITree botBT;
    private bool mustBeKilled;

    public float ChaseSpeed { get => chaseSpeed; set => chaseSpeed = value; }
    public float WanderSpeed { get => wanderSpeed; set => wanderSpeed = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
 
    public float AttackRange {
        get => attackRange; set => attackRange = value;
    }
    public float FovRange {
        get => fovRange; set => fovRange = value;
    }
    public bool Kill {
        get => mustBeKilled; set => mustBeKilled = value;
    }

    public void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        botBT = new Testing_Bot_V1(null, gameObject);
        botBT.InitTree();
        IsDead = false;
        Kill = false;
    }

    public void Update() {
        if (mustBeKilled) {
            StartCoroutine(PlayDeadSequence());
        }
        botBT.UpdateNodes();
    }

    public void ApplyDamage(int damagedReceived) {
        health -= (health - damagedReceived > 0) ? damagedReceived : health;
        CheckHealthStatus();
    }

    protected void CheckHealthStatus() {
        if (health == 0) {
            isDead = true;
        }
    }

    private IEnumerator PlayDeadSequence() {
        navMeshAgent.speed = 0f;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSecondsRealtime(3f);
        mustBeKilled = false;
        gameObject.SetActive(false);
    }
}
