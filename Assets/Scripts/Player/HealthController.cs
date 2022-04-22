using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    
    [Header("Health Presets")]
    public int maxHealth = 500;
    public int currentHealth = 500;
    public bool isDead = false;

    [Header("Elements Associated")]
    public GameObject hudElementAssociated;
    public TextMeshProUGUI healthText;
    public Image healthImage;
    private static readonly Color goodHeathColor = new Color(0.3f, 1f, 0f, 0.6f);
    private static readonly Color normalHeathColor = new Color(1f, 0.6f, 0f, 0.6f);
    private static readonly Color badHeathColor = new Color(1f, 0f, 0f, 0.6f);

    void Start() {
        UpdateHealthHud();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            DecreaseHealth();
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            IncreaseHealth();
        }
        UpdateHealthHud();
    }

    public void DecreaseHealth() {
        if (currentHealth > 0) {
            currentHealth -= (currentHealth >= 25) ?
                25 :
                currentHealth;
        }
    }

    public void IncreaseHealth() {
        if (currentHealth < maxHealth) {
            currentHealth += (currentHealth <= maxHealth - 25) ?
                25 :
                (maxHealth - currentHealth);
        }
    }

    private void UpdateHealthHud() {
        float percentage = (float) currentHealth / maxHealth;
        if (percentage > 0.66f) {
            SetHudInGreen();
        } else if (percentage < 0.33f) {
            SetHudInRed();
        } else {
            SetHudInYellow();
        }
        healthText.SetText((percentage * 100)+"%");
    }

    private void SetHudInGreen() {
        healthText.color = goodHeathColor;
        healthImage.color = goodHeathColor;
    }

    private void SetHudInRed() {
        healthText.color = badHeathColor;
        healthImage.color = badHeathColor;
    }

    private void SetHudInYellow() {
        healthText.color = normalHeathColor;
        healthImage.color = normalHeathColor;
    }

    internal void RestorePlayerHealth() {
        currentHealth = maxHealth;
        UpdateHealthHud();
    }
}
