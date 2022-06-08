using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private RoundManager roundManager;
    [SerializeField] private SceneSelector sceneSelector;
    private bool isGamePaused = false;

    void Start() {
        roundManager = GetComponent<RoundManager>();
        sceneSelector = GetComponent<SceneSelector>();
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            isGamePaused = !isGamePaused;
            PauseGame();
        }
    }

    public void FinishGame() {
        roundManager.FinishGame();
    }

    public void RestartGame() {
        sceneSelector.PlayGame();
    }

    public void BackToMenu() {
        sceneSelector.GoToMainMenu();
    }

    public void PauseGame() {
        if (isGamePaused) {
            roundManager.PauseRound();
            isGamePaused = true;
            Time.timeScale = 0;
        } else {
            roundManager.ResumeRound();
            isGamePaused = false;
            Time.timeScale = 1;
        }
    }
}
