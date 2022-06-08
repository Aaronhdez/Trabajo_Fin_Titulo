using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] public List<GameObject> spawnPoints;
    [SerializeField] private Vector3 playerSpawnPoint;
    [SerializeField] public List<GameObject> enemySpawnPoints;
    private bool enemiesHaveBeenCreated = false;
    private EnemiesGenerator enemiesGenerator;
    private List<GameObject> enemiesToSpawn;
    private List<GameObject> enemiesCreated;

    private void Start() {
        enemiesGenerator = GetComponent<EnemiesGenerator>();
        enemiesToSpawn = enemiesGenerator.GetEnemiesList(PlayerPrefs.GetString("difficulty"));
        enemiesCreated = new List<GameObject>();
    }

    public void RespawnPlayer(GameObject player) {
        var randomPick = Random.Range(0, spawnPoints.Count);
        playerSpawnPoint = spawnPoints[randomPick].transform.position;
        player.transform.position = new Vector3(playerSpawnPoint.x,
            playerSpawnPoint.y+1, playerSpawnPoint.z);
    }

    public void RespawnEnemies(int enemyLifePoints, int enemyDamage, int roundsPlayed) {
        if (!enemiesHaveBeenCreated) {
            switch (PlayerPrefs.GetString("difficulty")) {
                case "1":
                    SpawnEasyMode();
                    break;
                case "2":
                    SpawnMediumMode();
                    break;
                case "3":
                    SpawnHardMode();
                    break;
                case "4":
                    SpawnVeryHardMode();
                    break;
            }
            enemiesHaveBeenCreated = true;
            ActivateEnemies(enemyLifePoints, enemyDamage, roundsPlayed);
        } else {
            ReloadPositions();
            ActivateEnemies(enemyLifePoints, enemyDamage, roundsPlayed);
        }
    }

    private void SpawnVeryHardMode() {
        var availablePoints = GetAvailablePoints();
        for (int i = 0; i < 20; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[0], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
        for (int i = 0; i < 10; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[1], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }

    private void SpawnHardMode() {
        var availablePoints = GetAvailablePoints();
        for (int i = 0; i < 20; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[0], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
        for (int i = 0; i < 10; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[1], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }

    private void SpawnMediumMode() {
        var availablePoints = GetAvailablePoints();
        for (int i = 0; i < 25; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[0], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
        for (int i = 0; i < 5; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[1], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }

    private void SpawnEasyMode() {
        var availablePoints = GetAvailablePoints();
        for (int i = 0; i < 30; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var newEnemy = Instantiate(enemiesToSpawn[0], nextEnemyPosition, nextEnemyRotation);
            enemiesCreated.Add(newEnemy);
            newEnemy.SetActive(false);
        }
    }

    private void ReloadPositions() {
        var availablePoints = GetAvailablePoints();
        for (int i = 0; i < 30; i++) {
            var randomPick = Random.Range(0, availablePoints.Count);
            var nextEnemyPosition = GetRandomPositionAround(enemySpawnPoints[randomPick].transform.position);
            var nextEnemyRotation = enemySpawnPoints[randomPick].transform.rotation;
            var enemyToApply = enemiesCreated[i];
            enemyToApply.transform.SetPositionAndRotation(nextEnemyPosition, nextEnemyRotation);
        }
    }
    internal void ActivateEnemies(int enemyLifePoints, int enemyDamage, int roundsPlayed) {
        foreach (GameObject enemy in enemiesCreated) {
            enemy.GetComponent<CapsuleCollider>().enabled = true;
            var enemyController = enemy.GetComponent<EnemyController>();
            enemyController.IsDead = false;
            enemyController.health = enemyLifePoints + (10 * roundsPlayed%3);
            enemyController.attackDamage = enemyDamage + (10 * roundsPlayed%5);
            enemy.SetActive(true);
        }
    }

    private Vector3 GetRandomPositionAround(Vector3 position) {
        var newX = Random.Range(position.x - 3, position.x + 3);
        var newZ = Random.Range(position.z - 3, position.z + 3);
        return new Vector3(newX, position.y, newZ);
    }

    private List<GameObject> GetAvailablePoints() {
        List<GameObject> availablePoints = new List<GameObject>();
        foreach (GameObject enemySpawnPoint in enemySpawnPoints) {
            if (Vector3.Distance(enemySpawnPoint.transform.position, playerSpawnPoint) > 20f) {
                availablePoints.Add(enemySpawnPoint);
            }
        }
        return availablePoints;
    }

    internal void PauseEnemies() {
        foreach (GameObject enemy in enemiesCreated) {
            var eAnimator = enemy.GetComponent<Animator>();
            eAnimator.enabled = false;
        }
    }

    internal void UnpauseEnemies() {
        foreach (GameObject enemy in enemiesCreated) {
            var eAnimator = enemy.GetComponent<Animator>();
            eAnimator.enabled = true;
        }
    }
}
