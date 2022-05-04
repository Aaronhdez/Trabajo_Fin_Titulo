using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    [Header("Round Elements")]
    [SerializeField] public GameObject spawnManager;
    [SerializeField] public GameObject player;
    [SerializeField] public PlayerController playerController;

    [SerializeField] public GameObject playerHUD;
    [SerializeField] public GameObject roundText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [Header("Round Parameters")]
    [SerializeField] public bool roundStarted = false;
    [SerializeField] public int enemiesAlive = 0;
    [SerializeField] public int roundsPlayed = 0; 

    [Header("Enemies Prefabs")]
    [SerializeField] private List<GameObject> enemies;
    private TextMeshProUGUI counterTextValue;

    public bool PlayingRound { get => roundStarted; set => roundStarted = value; }
    public int EnemiesAlive { get => enemiesAlive; set => enemiesAlive = value; }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        counterTextValue = roundText.GetComponentInChildren<TextMeshProUGUI>();
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
        ResetRoundComponents();
    }

    private void ResetRoundComponents() {
        counterTextValue.SetText("Press F4 to play");
        playerHUD.SetActive(false);
        roundText.SetActive(true);
        playerController.Lock();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F4) && !PlayingRound) {
            counterTextValue.SetText("3");
            Invoke(nameof(CountDown), 1);
        }
        
        if (PlayingRound) {
            enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
            UpdateEnemiesHUD();
        }

        if (enemiesAlive == 0 && PlayingRound) {
            EndRound();
        }
    }

    private void CountDown() {
        if (counterTextValue.text.Equals("Survive the horde!")) {
            StartRound();
            PlayingRound = true;
            return;
        } else {
            UpdateCounterText();
        }
    }

    public void StartRound() {
        Debug.Log("round Started");
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(enemies);
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        ActivateRoundComponents();
    }

    private void ActivateRoundComponents() {
        playerController.Unlock();
        playerHUD.SetActive(true);
        roundText.SetActive(false);
        enemiesText.SetText(enemiesAlive.ToString());
    }

    private void UpdateCounterText() {
        var value = int.Parse(counterTextValue.text) - 1;
        if (value > 0) {
            counterTextValue.SetText(value + "");
            Invoke(nameof(CountDown), 1);
        } else if (value == 0) {
            counterTextValue.SetText("Survive the horde!");
            Invoke(nameof(CountDown), 1);
        }
    }

    private void UpdateEnemiesHUD() {
        enemiesText.SetText(enemiesAlive.ToString());
    }

    public void EndRound() {
        ResetRoundComponents();
        roundsPlayed++;
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
        PlayingRound = false;
    }
}
