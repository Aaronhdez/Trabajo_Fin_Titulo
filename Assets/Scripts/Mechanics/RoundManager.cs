using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    [Header("Round Elements")]
    [SerializeField] public GameObject spawnManager;
    [SerializeField] public GameObject player;
    [SerializeField] public PlayerController playerController;

    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject roundText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [Header("Round Parameters")]
    [SerializeField] public bool roundStarted = false;
    [SerializeField] public int enemiesAlive = 0;
    [SerializeField] public int roundsPlayed = 0; 

    [Header("Enemies Prefabs")]
    [SerializeField] private List<GameObject> enemies;

    public bool RoundStarted { get => roundStarted; set => roundStarted = value; }
    public int EnemiesAlive { get => enemiesAlive; set => enemiesAlive = value; }

    void Start() {
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
        player = GameObject.Find("PlayerPrefab");
        playerController = player.GetComponent<PlayerController>();
        roundStarted = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F4) && !RoundStarted) {
            roundText.GetComponentInChildren<TextMeshProUGUI>().SetText("3");
            Invoke(nameof(StartCountDown), 1);
        } else { 
            ProcessRoundActions();
        }
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesAlive == 0 && RoundStarted) {
            EndRound();
        }
    }

    private void ProcessRoundActions() {
        if (!RoundStarted) {
            player.GetComponent<PlayerController>().Lock();
            playerHUD.SetActive(false);
        } else {
            ActivateRoundComponents();
            UpdateEnemiesHUD();
        }
    }

    private void ActivateRoundComponents() {
        playerHUD.SetActive(true);
        roundText.SetActive(false);
        playerController.Unlock();
    }

    private void DisactivateRoundComponents() {
        //playerHUD.SetActive(false);
        //roundText.SetActive(true);
        playerController.Lock();
    }

    private void UpdateEnemiesHUD() {
        enemiesText.SetText(enemiesAlive.ToString());
    }

    private void StartCountDown() {
        TextMeshProUGUI roundTextValue = roundText.GetComponentInChildren<TextMeshProUGUI>();
        if (roundTextValue.text.Equals("Survive the horde!")) {
            StartRound();
            return;
        } else {
            UpdateRoundText(roundTextValue);
        }
    }

    private void UpdateRoundText(TextMeshProUGUI roundTextValue) {
        var value = int.Parse(roundTextValue.text) - 1;
        if (value > 0) {
            roundTextValue.SetText(value + "");
            Invoke(nameof(StartCountDown), 1);
        } else if (value == 0) {
            roundTextValue.SetText("Survive the horde!");
            Invoke(nameof(StartCountDown), 1);
        }
    }

    public void StartRound() {
        RoundStarted = true;
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EndRound() {
        DisactivateRoundComponents();
        roundsPlayed++;
        RoundStarted = false;
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
    }
}
