using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    [Header("Round Elements")]
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject roundText;
    [SerializeField] private TextMeshProUGUI enemiesText;

    [Header("Round Parameters")]
    [SerializeField] private bool roundStarted = false;
    [SerializeField] private int enemiesAlive = 0;

    [Header("Enemies Prefabs")]
    [SerializeField] private List<GameObject> enemies;


    void Start() {
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
    }

    void Update() {
        if (!roundStarted) {
            player.GetComponent<PlayerController>().Lock();
            playerHUD.SetActive(false);
        } else {
            player.GetComponent<PlayerController>().Unlock();
            playerHUD.SetActive(true);
            roundText.SetActive(false);
            var enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
            enemiesText.SetText(enemiesRemaining.ToString());
        }

        if (Input.GetKeyDown(KeyCode.F4)) {
            roundText.GetComponentInChildren<TextMeshProUGUI>().SetText("3");
            Invoke(nameof(StartCountDown), 1);
        }
    }

    private void StartCountDown() {
        TextMeshProUGUI roundTextValue = roundText.GetComponentInChildren<TextMeshProUGUI>();
        if (roundTextValue.text.Equals("Survive the horde!")) {
            StartRound();
            return;
        }
        var value = int.Parse(roundTextValue.text);
        value -= 1;
        if (value > 0) {
            roundTextValue.SetText(value + "");
            Invoke(nameof(StartCountDown), 1);
        } else if (value == 0) {
            roundTextValue.SetText("Survive the horde!");
            Invoke(nameof(StartCountDown), 1);
        }
    }

    internal void StartRound() {
        roundStarted = true;
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
    }
}
