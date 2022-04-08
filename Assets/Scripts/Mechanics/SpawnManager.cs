using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] public List<GameObject> spawnPoints;
    [SerializeField] private Vector3 playerSpawnPoint;
    [SerializeField] public List<GameObject> enemySpawnPoints;

    public void RespawnPlayer(GameObject player) {
        var randomPick = Random.Range(0, spawnPoints.Count);
        playerSpawnPoint = spawnPoints[randomPick].transform.position;
        player.transform.position = new Vector3(playerSpawnPoint.x,
            playerSpawnPoint.y, playerSpawnPoint.z);
    }

    internal void RespawnEnemies(List<GameObject> enemies) {

    }
}
