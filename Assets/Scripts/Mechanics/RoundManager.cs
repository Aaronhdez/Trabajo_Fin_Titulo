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
            playerHUD.SetActive(false);
        } else {
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
        if (roundText.GetComponentInChildren<TextMeshProUGUI>().text.Equals("Survive the horde!")) {
            StartRound();
        }
        var value = int.Parse(roundText.GetComponentInChildren<TextMeshProUGUI>().text);
        value -= 1;
        if (value > 0) {
            roundText.GetComponentInChildren<TextMeshProUGUI>().SetText(value + "");
            Invoke(nameof(StartCountDown), 1);
        } else if (value == 0) {
            roundText.GetComponentInChildren<TextMeshProUGUI>().SetText("Survive the horde!");
            Invoke(nameof(StartCountDown), 1);
        }
    }

    internal void StartRound() {
        roundStarted = true;
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
    }
}
