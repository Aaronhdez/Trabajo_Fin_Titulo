using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth = 500;
    public bool isDead = false;
    public GameObject hudElementAssociated;
    public TextMeshProUGUI healthText;
    public Image healthImage;
    private static readonly Color goodHeathColor = new Color(0.3f, 1f, 0f, 0.6f);
    private static readonly Color normalHeathColor = new Color(0.3f, 1f, 0f, 0.6f);
    private static readonly Color badHeathColor = new Color(0.3f, 1f, 0f, 0.6f);

    void Start() {
        UpdateHealthHud();
    }

    private void UpdateHealthHud() {
        float percentage = currentHealth / maxHealth;
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
    }

    internal void RestorePlayerHealth() {
        currentHealth = maxHealth;
        UpdateHealthHud();
    }
}
