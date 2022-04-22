using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HudController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthIcon;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Ammo")]
    [SerializeField] private List<TextMeshProUGUI> ammoTexts;
    [SerializeField] private List<GameObject> ammoEntities;
    private Dictionary<int, Action> hudAmmoActions;

    public void PresetHUD() {
        ammoEntities = new List<GameObject>(GameObject.FindGameObjectsWithTag("HUDAmmoEntity"));
        healthIcon = GameObject.Find("HealthHud").GetComponentInChildren<Image>();
        healthText = GameObject.Find("HealthHud").GetComponentInChildren<TextMeshProUGUI>();
        //PresetDictionary();
        PresetUI();
    }

    /*private void PresetDictionary() {
        hudAmmoActions = new Dictionary<int, Action>();
        hudAmmoActions.Add(0, ReloadRifleHud);
        hudAmmoActions.Add(1, ReloadGunHud);
    }*/

    private void PresetUI() {
        ReloadHealthHud();
        ReloadAmmoHuds();
        DisplayGunHUD(0);
    }
    public void ReloadHealthHud() {
        healthIcon.color = new Color(0f, 1f, 0f, 0.6f);
        healthText.SetText("100%");
    }

    public void ReloadAmmoHuds() {
        for (int index = 0; index < hudAmmoActions.Count; index++) {
            hudAmmoActions[index]();
        }
    }

    /*private void ReloadRifleHud() {
        ammoEntities[0].GetComponentInChildren<TextMeshProUGUI>().SetText("40/240");
    }

    private void ReloadGunHud() {
        ammoEntities[1].GetComponentInChildren<TextMeshProUGUI>().SetText("10/60");
    }*/

    public void UpdateGunHUD(TextMeshPro textField, String text) {
        textField.SetText(text);
    }

    public void DisplayGunHUD(int gunIndex) {
        for (int index = 0; index < ammoEntities.Count; index++) {
            if (index == gunIndex) {
                continue;
            }
            ammoEntities[index].SetActive(false);
        }
    }
}
