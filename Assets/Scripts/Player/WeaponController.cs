using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour {

    [Header("Weapon Presets")]
    public int maxAmountOfBullets;
    public int maxAmountInMagazine;
    public int amountInMagazine;
    public int amountOfBullets;
    public float fireRate = 0.5f;
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
    //public ParticleSystem muzzleFlash;
    //public GameObject muzzleFlashObject;
    public ParticleSystem impactEffect;
    public ParticleSystem bloodEffect;

    [Header("Sounds Associated")]
    public SoundController soundController;
    public AudioSource shotSound;
    public AudioSource reloadSound;

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
        if(gunText != null) { 
            gunText.SetText(amountInMagazine + "/" + amountOfBullets);
            if (mustReplenish) {
                gunImage.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
                gunText.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);
            } else {
                gunImage.color = new Color(1f, 1f, 1f, 0.6f);
                gunText.color = new Color(1f, 1f, 1f, 0.6f);
            }
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

    public void Fire() {
        if(Time.time > fireRate + lastShot) {
            amountInMagazine -= 1;
            lastShot = Time.time;
            RaycastShot();
        }
    }

    private void RaycastShot() {
        if (Physics.Raycast(pointingCamera.position, pointingCamera.forward,
                        out impactInfo, weaponRange)) {

            Ray ray = new Ray(pointingCamera.position, pointingCamera.forward);
            Debug.DrawLine(ray.origin, impactInfo.point, Color.red, 0.45f);

            if (impactInfo.collider.tag.Equals("Enemy")) {
                PlayShootAnimation();
            } else {
                PlayImpactAnimation();
            }
            shotSound.Play();
            ApplyDamageOnTarget(impactInfo);
        }
    }

    private void PlayShootAnimation() {
        //muzzleFlash.Play();
    }

    private void ApplyDamageOnTarget(RaycastHit impactInfo) {
        Instantiate(bloodEffect, impactInfo.point, Quaternion.LookRotation(impactInfo.normal));
        if (impactInfo.collider.GetComponent<EnemyController>() != null) { 
            impactInfo.collider.GetComponent<EnemyController>().ApplyDamage(weaponDamage);
        }
    }

    private void PlayImpactAnimation() {
        Instantiate(impactEffect, impactInfo.point, Quaternion.LookRotation(impactInfo.normal));
    }

    public void Reload() {
        if (amountInMagazine < maxAmountInMagazine) { 
            int amountToReload = maxAmountInMagazine - amountInMagazine;
            if(amountOfBullets > 0) {
                reloadSound.Play();
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

    public void SetInactive() {
        hudElementAssociated.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void SetActive() {
        hudElementAssociated.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void ReplenishAmmo() {
        reloadSound.Play();
        RestoreAmmoValues();
    }

    public void RestoreAmmoValues() {
        amountOfBullets = maxAmountOfBullets;
        amountInMagazine = maxAmountInMagazine;
        mustReplenish = false;
    }
}
