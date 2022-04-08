using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> spawnPoints;
    [SerializeField] public GameObject player;

    void Start() {
        RespawnPlayer();
    }

    private void RespawnPlayer() {
        var randomPick = Random.Range(0, spawnPoints.Count);
        var spawnPoint = spawnPoints[randomPick].transform.position;
        player.transform.position = new Vector3(spawnPoint.x,
            spawnPoint.y, spawnPoint.z);
    }
}
