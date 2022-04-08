using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] public List<GameObject> spawnPoints;

    public void RespawnPlayer(GameObject player) {
        var randomPick = Random.Range(0, spawnPoints.Count);
        var spawnPoint = spawnPoints[randomPick].transform.position;
        player.transform.position = new Vector3(spawnPoint.x,
            spawnPoint.y, spawnPoint.z);
    }

    internal void RespawnEnemies(List<GameObject> enemies) {
    }
}
