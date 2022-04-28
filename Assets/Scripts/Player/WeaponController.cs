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
    public int weaponDamage = 15;
    public int weaponRange = 15;

    [Header("Elements Associated")]
    public GameObject hudElementAssociated;
    public TextMeshProUGUI gunText;
    public Image gunImage;
    public ParticleSystem muzzleFlash;
    public GameObject muzzleFlashObject;
    public ParticleSystem impactEffect;

    [Header("Raycasting Elements")]
    public RaycastHit impactInfo;
    public Transform pointingCamera;

    void Start() {
        amountOfBullets = maxAmountOfBullets;
        amountInMagazine = maxAmountInMagazine;
        //muzzleFlash.Stop();
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
            RaycastShot();
            amountInMagazine -= 1;
            lastShot = Time.time;
        }
    }

    private void RaycastShot() {
        if (Physics.Raycast(pointingCamera.position, pointingCamera.forward,
                        out impactInfo, weaponRange)) {
            PlayShootAnimation();
            //IF TARGET IS ENEMY, FIND PROPER SCRIPT AND PLAY GETDAMAGED()
            ApplyDamageOnTarget();
            //ELSE PLAY IMPACT EFFECT ON IMPACT POSITION
            PlayImpactAnimation();
        }
    }

    private void PlayShootAnimation() {
        //muzzleFlash.Play();
    }

    private void ApplyDamageOnTarget() {
        //throw new NotImplementedException();
    }

    private void PlayImpactAnimation() {
        Instantiate(impactEffect, impactInfo.point, Quaternion.LookRotation(impactInfo.normal));
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
