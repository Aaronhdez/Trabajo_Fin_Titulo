using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Player")]
    [SerializeField] public GameObject player;
    [SerializeField] public List<GameObject> enemies;
    [SerializeField] public GameObject spawnManager;
    [SerializeField] public GameObject hudManager;

    void Start() {
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
    }
}
