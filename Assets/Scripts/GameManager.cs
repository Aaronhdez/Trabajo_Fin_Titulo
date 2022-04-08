using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> spawnPoints;
    [SerializeField] public GameObject player; 
    void Start() {
        var randomPick = Random.Range(0, spawnPoints.Count + 1);
        var spawnPoint = spawnPoints[randomPick].transform;
        Instantiate(player, spawnPoint.position, 
            spawnPoint.rotation, spawnPoint);
        player.SetActive(true);
    }
}
