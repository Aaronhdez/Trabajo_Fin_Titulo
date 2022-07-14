using BehaviorTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BotController : MonoBehaviour, IPlayerController
{
    public string SceneName = "";
    [Header("General Properties")]
    [SerializeField] protected float wanderSpeed;
    [SerializeField] protected float chaseSpeed;
    public int health;
    public int attackDamage;
    [SerializeField] protected bool isDead;
    [SerializeField] protected float fovRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fireRate;

    [Header("Game Instances")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected ITree botBT;
    private bool mustBeKilled;

    //TestResults
    private int impacts = 0;
    private int damageReceived = 0;
    private int enemiesDefeated = 0;
    private int timesHealed = 0;

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
    public float FireRate { 
        get => fireRate; set => fireRate = value; 
    }

    public void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        botBT = new Testing_Bot_V1(null, gameObject);
        botBT.InitTree();
        IsDead = false;
        Kill = false;
    }

    public void Update() {
        if (FindObjectsOfType<EnemyController>().Length == 0) {
            SceneManager.LoadScene(SceneName);
            enemiesDefeated = 30;
            SaveTestData("victory");
        }
        if (isDead) {
            enemiesDefeated = 30 - FindObjectsOfType<EnemyController>().Length;
            SaveTestData("defeat");
            SceneManager.LoadScene(SceneName);
        }
        botBT.UpdateNodes();
    }

    public void ApplyDamage(int damage) {
        health -= (health - damage > 0) ? damage : health;
        impacts += 1;
        damageReceived += damage;
        CheckHealthStatus();
    }

    protected void CheckHealthStatus() {
        if (health == 0) {
            isDead = true;
        }
    }

    public bool LowHealth() {
        return health < 50;
    }
    public void RestoreHealth() {
        timesHealed += 1;
        health = 100;
    }

    private void SaveTestData(string result) {
        string testFileName = @"TestsResults_"+ SceneName + ".txt";
        if (!File.Exists(testFileName)) {
            File.Create(testFileName);
            using (StreamWriter sw = new StreamWriter(testFileName, append: true)) {
                sw.WriteLine("Result; Impacts; Damage Received; Enemies Defated; Times Healed;");
            }

        }

        var testResults =
            result + "; "
            + impacts + "; "
            + damageReceived + "; "
            + enemiesDefeated + "; "
            + timesHealed + "; ";

        using (StreamWriter sw = new StreamWriter(testFileName, append: true)) {
            sw.WriteLine(testResults);
        }

    }

}
