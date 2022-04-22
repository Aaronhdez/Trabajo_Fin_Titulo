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

    public void PresetHUD() {
        ammoEntities = new List<GameObject>(GameObject.FindGameObjectsWithTag("HUDAmmoEntity"));
        healthIcon = GameObject.Find("HealthHud").GetComponentInChildren<Image>();
        healthText = GameObject.Find("HealthHud").GetComponentInChildren<TextMeshProUGUI>();
        //PresetDictionary();
        PresetUI();
    }

    private void PresetUI() {
        ReloadHealthHud();
        DisplayGunHUD(0);
    }
    public void ReloadHealthHud() {
        healthIcon.color = new Color(0f, 1f, 0f, 0.6f);
        healthText.SetText("100%");
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
