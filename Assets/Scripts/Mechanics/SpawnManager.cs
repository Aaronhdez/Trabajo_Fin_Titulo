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

    public void RespawnEnemies(List<GameObject> enemies) {
        var availablePoints = getAvailablePoints();
        for (int i = 0; i < 16; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            Instantiate(enemies[0], nextEnemyPosition, nextEnemyRotation);
        }
    }

    private Vector3 GetRandomPositionAround(Vector3 position) {
        var newX = Random.Range(position.x - 3, position.x + 3);
        var newZ = Random.Range(position.z - 3, position.z + 3);
        return new Vector3(newX, position.y, newZ);
    }

    private List<GameObject> getAvailablePoints() {
        List<GameObject> availablePoints = new List<GameObject>();
        foreach (GameObject enemySpawnPoint in enemySpawnPoints) {
            if (Vector3.Distance(enemySpawnPoint.transform.position, playerSpawnPoint) > 20f) {
                availablePoints.Add(enemySpawnPoint);
            }
        }
        return availablePoints;
    }
}
