using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelector : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject optionsCanvas;
    private GameObject mainCamera;
    private GameObject mainMenuPosition;
    private GameObject optionsPosition;
    private GameObject difficultyDropDown;
    private GameObject volumeScrollbar;
    private GameObject mouseScrollbar;
    private Vector3 nextPosition;

    public void Start() {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu")) { 
            LoadMainSceneElements();
        }
    }

    private void LoadMainSceneElements() {
        mainCanvas = GameObject.Find("Elements").GetComponent<Elements>().mainCanvas;
        optionsCanvas = GameObject.Find("Elements").GetComponent<Elements>().optionsCanvas;
        mainCamera = GameObject.Find("Elements").GetComponent<Elements>().camera;
        mainMenuPosition = GameObject.Find("Elements").GetComponent<Elements>().mainMenuPosition;
        optionsPosition = GameObject.Find("Elements").GetComponent<Elements>().optionsPosition;
        difficultyDropDown = GameObject.Find("Elements").GetComponent<Elements>().difficultyDropDown;
        volumeScrollbar = GameObject.Find("Elements").GetComponent<Elements>().volumeScrollbar;
        mouseScrollbar = GameObject.Find("Elements").GetComponent<Elements>().mouseScrollbar;
        nextPosition = optionsPosition.transform.position;
    }

    public void GoToMainMenu() {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void PlayGame() {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void ShowOptions() {
        nextPosition = optionsPosition.transform.position;
        mainCamera.transform.position = nextPosition;
        mainCamera.transform.Rotate(new Vector3(0f, -30f, 0f), Space.World);
        optionsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }

    public void ShowMainMenu() {
        nextPosition = mainMenuPosition.transform.position;
        mainCamera.transform.position = nextPosition;
        mainCamera.transform.Rotate(new Vector3(0f, 30f, 0f), Space.World);
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void SetDifficulty() {
        var dropdown = difficultyDropDown.GetComponent<TMP_Dropdown>();
        PlayerPrefs.SetString("difficulty", ""+(dropdown.value+1));
    }

    public void SetVolume() {
        var scrollbar = volumeScrollbar.GetComponent<Scrollbar>();
        PlayerPrefs.SetString("volume", "" + scrollbar.value);
    }

    public void SetMouseSense() {
        var scrollbar = mouseScrollbar.GetComponent<Scrollbar>();
        PlayerPrefs.SetString("mouseSense", "" + scrollbar.value);
    }
}
