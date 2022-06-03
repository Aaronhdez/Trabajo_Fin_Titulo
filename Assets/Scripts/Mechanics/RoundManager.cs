using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour {

    [Header("Round Elements")]
    public GameObject spawnManager;
    public GameObject player;
    public PlayerController playerController;
    [SerializeField] private int enemyLifePoints;
    [SerializeField] private int enemyDamage;

    public GameObject playerHUD;
    public GameObject roundText;
    public GameObject roundCounter;
    public GameObject enemiesHUD;

    [Header("Round Parameters")]
    [SerializeField] private bool roundStarted = false;
    [SerializeField] private bool roundFinished = false;
    public int enemiesAlive = 0;
    public int roundsPlayed = 0; 

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI roundPlayedText;
    [SerializeField] private TextMeshProUGUI counterTextValue;

    public bool PlayingRound { get => roundStarted; set => roundStarted = value; }
    public int EnemiesAlive { get => enemiesAlive; set => enemiesAlive = value; }
    public bool RoundFinished { get => roundFinished; set => roundFinished = value; }

    void Start() {
        enemiesText = GameObject.Find("EnemiesText").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        counterTextValue = roundText.GetComponentInChildren<TextMeshProUGUI>();
        RespawnPlayer();
        ResetRoundComponents();
        ResetCounterText();
    }

    private void RespawnPlayer() {
        spawnManager.GetComponent<SpawnManager>().RespawnPlayer(player);
    }

    private void ResetRoundComponents() {
        playerHUD.SetActive(false);
        roundText.SetActive(true);
        playerController.Lock();
    }

    private void ResetCounterText() {
        counterTextValue.SetText("Press F4 to play");
    }

    void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.F4) && !PlayingRound) {
            counterTextValue.SetText("3");
            Invoke(nameof(CountDown), 1);
        }
        
        if (PlayingRound) {
            enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
            UpdateEnemiesHUD();
        }

        if (enemiesAlive == 0 && PlayingRound) {
            PlayingRound = false;
            roundFinished = true;
        }

        if (roundFinished) {
            playerHUD.SetActive(false);
            roundText.SetActive(true);
            roundFinished = false;
            counterTextValue.SetText("Congratulations! you survived, for now...");
            Invoke(nameof(StartNewRoundCounter), 5);
        }
    }

    private void CountDown() {
        if (counterTextValue.text.Equals("Survive the horde!")) {
            StartRound();
            enemiesText.SetText(enemiesAlive.ToString());
            return;
        } else {
            UpdateCounterText();
        }
    }

    public void StartRound() {
        spawnManager.GetComponent<SpawnManager>().RespawnEnemies(
            enemyLifePoints,
            enemyDamage,
            roundsPlayed);
        enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        ActivateRoundComponents();
    }

    private void ActivateRoundComponents() {
        playerController.Unlock();
        playerHUD.SetActive(true);
        roundText.SetActive(false);
        PlayingRound = true;
    }

    private void UpdateCounterText() {
        var value = int.Parse(counterTextValue.text) - 1;
        if (value > 0) {
            counterTextValue.SetText(value.ToString());
            Invoke(nameof(CountDown), 1);
        } else if (value == 0) {
            counterTextValue.SetText("Survive the horde!");
            Invoke(nameof(CountDown), 1);
        }
    }

    private void UpdateEnemiesHUD() {
        enemiesText.SetText(enemiesAlive.ToString());
    }

    private void StartNewRoundCounter() {
        if(counterTextValue.text.Equals("Congratulations! you survived, for now...")) {
            counterTextValue.SetText("5");
            Invoke(nameof(StartNewRoundCounter), 1);
        } else {
            var value = int.Parse(counterTextValue.text) - 1;
            if (value > 0) {
                counterTextValue.SetText(value.ToString());
                Invoke(nameof(StartNewRoundCounter), 1);
                return;
            } else {
                ResetCounterText();
                EndRound();
            }
        }
    }

    public void EndRound() {
        ResetRoundComponents();
        RespawnPlayer();
        roundsPlayed++;
        PlayingRound = false;
    }
}
