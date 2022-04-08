using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public List<GameObject> enemies;
    [SerializeField] public GameObject spawnManager;

    void Start() {
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
        //spawnManager.GetComponent<SpawnManager>().RespawnProps(enemies);
    }

    
}
