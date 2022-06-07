using BehaviorTree;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_BT : EnemyController {
    
    private bool mustBeKilled;

    [Header("Barker Properties")]
    [SerializeField] private float maxDistanceToBeAlerted;
    [SerializeField] private float distanceToSpreadAlert;
    [SerializeField] private float timeToRespawnAlert;
    public bool hasAlreadyAlerted;

    [Header("Tree Properties")]
    [SerializeField] public string treeToLoad;
    public GameObject agent;
    private ITree tree;

    public bool Kill { 
        get => mustBeKilled; set => mustBeKilled = value; }

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


    public void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        tree = new TreeGenerator(agent).ConstructTreeFor(treeToLoad);
        tree.InitTree();
        IsDead = false;
        Kill = false;
    }

    public void Update() {
        if (mustBeKilled) {
            StartCoroutine(PlayDeadSequence());
        }
        if (HasAlreadyAlerted) {
            navMeshAgent.speed = 0f;
            Debug.Log(gameObject + "agente ha alertado");
            StartCoroutine(PlayAlertSequence());
        }
        tree.UpdateNodes();
    }

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

    private IEnumerator PlayDeadSequence() {
        animator.Play("Z_FallingBack");
        navMeshAgent.speed = 0f;
        yield return new WaitForSecondsRealtime(3f);
        mustBeKilled = false;
        gameObject.SetActive(false);
    }

    private IEnumerator PlayAlertSequence() {
        yield return new WaitForSecondsRealtime(timeToRespawnAlert);
        HasAlreadyAlerted = false;
    }

}
