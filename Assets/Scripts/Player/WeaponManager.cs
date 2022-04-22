using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Weapons Available")]
    [SerializeField] public List<GunController> weaponsAssociated;
    [SerializeField] private GunController currentWeapon;
    [SerializeField] private int currentWeaponIndex;

    public GunController CurrentWeapon { 
        get => currentWeapon; 
        set => currentWeapon = value; 
    }

    void Start() {
        weaponsAssociated = new List<GunController>(GetComponentsInChildren<GunController>());
        currentWeaponIndex = 0;
        DisactivateRestOfWeapons(currentWeaponIndex);
    }

    void LateUpdate() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            currentWeaponIndex = (currentWeaponIndex + 1) % weaponsAssociated.Count;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            currentWeaponIndex = (currentWeaponIndex - 1) % weaponsAssociated.Count;
        }
    }

    private void DisactivateRestOfWeapons(int currentWeaponIndex) {
        int weaponIndex = currentWeaponIndex;
        int index = 0;
        foreach (GunController weaponController in weaponsAssociated) {
            if (index != weaponIndex) {
                weaponController.SetInactive();
            }
            index++;
        }
        currentWeapon = weaponsAssociated[currentWeaponIndex];
        currentWeapon.SetActive();
    }

    public void SwitchWeapon() {


    }

}
