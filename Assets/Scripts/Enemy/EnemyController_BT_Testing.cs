using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController_BT_Testing : EnemyController {

    [Header("Tree Properties")]
    [SerializeField] public string treeToLoad;
    public GameObject agent;
    private ITree tree;

    public void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        tree = new TreeGenerator(agent).ConstructTreeFor(treeToLoad);
        tree.InitTree();
        IsDead = false;
        Kill = false;
    }

    public void Update() {
        if (isDead) {
            Destroy(gameObject);
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
            isDead = true;
        }
    }

    private IEnumerator PlayAlertSequence() {
        navMeshAgent.speed = 0f;
        yield return new WaitForSecondsRealtime(10);
        HasAlreadyAlerted = false;
    }
}
