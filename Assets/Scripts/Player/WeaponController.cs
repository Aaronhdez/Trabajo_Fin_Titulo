using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

    [Header("Weapon Presets")]
    public int maxAmountOfBullets;
    public int maxAmountInMagazine;
    public int amountInMagazine;
    public int amountOfBullets;
    public float fireRate = 0.01f;
    public float lastShot = 0.0f;

    [Header("Weapon Status")]
    public bool mustReload = false;
    public bool mustReplenish = false;
    public int damage = 15;

    [Header("Elements Associated")]
    public GameObject hudElementAssociated;
    public TextMeshProUGUI gunText;
    public Image gunImage;

    private void Start() {
        amountOfBullets = maxAmountOfBullets;
        amountInMagazine = maxAmountInMagazine;
    }

    private void LateUpdate() {
        UpdateGunHUD();
    }

    private void UpdateGunHUD() {
        gunText.SetText(amountInMagazine + "/" + amountOfBullets);
        if (mustReplenish) {
            gunImage.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
            gunText.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
        } else {
            gunImage.color = new Color(1f, 1f, 1f, 0.6f);
            gunText.color = new Color(1f, 1f, 1f, 0.6f);
        }
    }

    public void Shoot() {
        if (amountInMagazine > 0) {
            Fire();
            if (amountInMagazine == 0) {
                mustReload = true;
                if (amountOfBullets == 0) {
                    mustReplenish = true;
                }
            }
        }
    }

    private void Fire() {
        if(Time.time > fireRate + lastShot) {
            amountInMagazine -= 1;
            lastShot = Time.time;
        }
    }

    public void Reload() {
        if(amountInMagazine < maxAmountInMagazine) { 
            int amountToReload = maxAmountInMagazine - amountInMagazine;
            if(amountOfBullets > 0) {
                UpdateGunQuantities(amountToReload);
            }
        }
    }

    private void UpdateGunQuantities(int amountToReload) {
        if (amountOfBullets > 0) {
            if (amountOfBullets > amountToReload) {
                amountInMagazine += amountToReload;
                amountOfBullets -= amountToReload;
            } else {
                amountInMagazine += amountOfBullets;
                amountOfBullets -= amountOfBullets;
            }
        }
    }

    internal void SetInactive() {
        hudElementAssociated.SetActive(false);
        this.gameObject.SetActive(false);
    }

    internal void SetActive() {
        hudElementAssociated.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void ReplenishAmmo() {
        amountOfBullets = maxAmountOfBullets;
        amountInMagazine = maxAmountInMagazine;
        mustReplenish = false;
        UpdateGunHUD();
    }

}
