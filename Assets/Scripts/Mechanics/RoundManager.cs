using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    [Header("Round Elements")]
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [Header("Enemies Prefabs")]
    [SerializeField] private List<GameObject> enemies;


    void Start() {
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
    }

    void Update() {
        var enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesText.SetText(enemiesRemaining.ToString());
    }

    internal void StartRound() {
        Debug.Log("Round Started");
    }
}
