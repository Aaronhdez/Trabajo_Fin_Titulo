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
            if(currentHealth > 0) { 
                currentHealth -= (currentHealth >= 25) ?
                    25 :
                    currentHealth;
            }
        }
        if (Input.GetKeyDown(KeyCode.J)) {
            if (currentHealth < maxHealth) {
                currentHealth += (currentHealth <= maxHealth - 25) ?
                    25 :
                    (maxHealth - currentHealth);
            }
        }
        UpdateHealthHud();
    }

    private void UpdateHealthHud() {
        float percentage = (float) currentHealth / maxHealth;
        if (percentage > 0.66f) {
            healthText.color = goodHeathColor;
            healthImage.color = goodHeathColor;
        } else if (percentage < 0.33f) {
            healthText.color = badHeathColor;
            healthImage.color = badHeathColor;
        } else {
            healthText.color = normalHeathColor;
            healthImage.color = normalHeathColor;
        }
        healthText.SetText((percentage * 100)+"%");
    }

    internal void RestorePlayerHealth() {
        currentHealth = maxHealth;
        UpdateHealthHud();
    }
}
