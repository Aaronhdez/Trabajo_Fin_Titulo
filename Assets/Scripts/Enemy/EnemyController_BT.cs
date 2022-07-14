using BehaviorTree;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_BT : EnemyController {

    [Header("Barker Properties")]
    [SerializeField] private float timeToRespawnAlert;

    [Header("Tree Properties")]
    [SerializeField] public string treeToLoad;
    public GameObject agent;
    private ITree tree;

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
        if (Kill) {
            StartCoroutine(PlayDeadSequence());
        }
        if (HasAlreadyAlerted) {
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
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSecondsRealtime(3f);
        Kill = false;
        gameObject.SetActive(false);
    }

    private IEnumerator PlayAlertSequence() {
        navMeshAgent.speed = 0f;
        animator.Play("Z_Attack");
        yield return new WaitForSecondsRealtime(timeToRespawnAlert);
        HasAlreadyAlerted = false;
    }

}
