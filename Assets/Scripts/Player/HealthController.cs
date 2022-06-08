using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {
    [Header("Health Presets")]
    public int maxHealth = 500;
    public int currentHealth = 500;
    public int healthChange = 25;
    private bool isDead = false;
    public bool IsDead { get => isDead; set => isDead = value; }

    [Header("Elements Associated")]
    public GameObject hudElementAssociated;
    public TextMeshProUGUI healthText;
    public Animator hitPanelAnimator;
    public Image healthImage;
    public Image healthDamage;
    private static readonly Color goodHeathColor = new Color(0.3f, 1f, 0f, 0.6f);
    private static readonly Color normalHeathColor = new Color(1f, 0.6f, 0f, 0.6f);
    private static readonly Color badHeathColor = new Color(1f, 0f, 0f, 0.6f);
    private static readonly Color badHeathColorCritical = new Color(1f, 0f, 0f, 0.2f);
    private static readonly Color badHeathColorNormal = new Color(1f, 0f, 0f, 0f);


    void Start() {
        UpdateHealthHud();
    }

    void Update() {
        UpdateHealthHud();
        CheckDeadStatus();
    }

    private void CheckDeadStatus() {
        IsDead = (currentHealth == 0);
    }

    public void DecreaseHealth(int damageToApply) {
        if (currentHealth > 0) {
            StartCoroutine(Animate());
            currentHealth -= (currentHealth >= damageToApply) ?
                damageToApply :
                currentHealth;
        }
    }

    private IEnumerator Animate() {
        hitPanelAnimator.gameObject.SetActive(true);
        hitPanelAnimator.Play("Hit");
        yield return new WaitForSeconds(1);
        hitPanelAnimator.gameObject.SetActive(false);
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
        healthText.SetText((int) (percentage * 100)+"%");
    }

    private void SetHudInGreen() {
        healthText.color = goodHeathColor;
        healthImage.color = goodHeathColor;
        healthDamage.color = badHeathColorNormal;
    }

    private void SetHudInRed() {
        healthText.color = badHeathColor;
        healthImage.color = badHeathColor;
        healthDamage.color = badHeathColorCritical;
    }

    private void SetHudInYellow() {
        healthText.color = normalHeathColor;
        healthImage.color = normalHeathColor;
        healthDamage.color = badHeathColorNormal;
    }

    internal void RestorePlayerHealth() {
        currentHealth = maxHealth;
    }
}
