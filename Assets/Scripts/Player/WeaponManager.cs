using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Weapons Available")]
    [SerializeField] public List<WeaponController> weaponsAssociated;
    [SerializeField] private WeaponController currentWeapon;
    [SerializeField] private int currentWeaponIndex;

    public WeaponController CurrentWeapon { 
        get => currentWeapon; 
        set => currentWeapon = value; 
    }

    void Start() {
        weaponsAssociated = new List<WeaponController>(GetComponentsInChildren<WeaponController>());
        currentWeaponIndex = 0;
        DisactivateRestOfWeapons(currentWeaponIndex);
        SetActiveWeapon(currentWeaponIndex);
    }

    private void SetActiveWeapon(int currentWeaponIndex) {
        currentWeapon = weaponsAssociated[currentWeaponIndex];
        currentWeapon.SetActive();
    }

    private void DisactivateRestOfWeapons(int currentWeaponIndex) {
        int weaponIndex = currentWeaponIndex;
        int index = 0;
        foreach (WeaponController weaponController in weaponsAssociated) {
            if (index != weaponIndex) {
                weaponController.SetInactive();
            }
            index++;
        }
    }
    void LateUpdate() {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            currentWeaponIndex = Math.Abs((currentWeaponIndex + 1) % weaponsAssociated.Count);
            SwitchWeapon();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            currentWeaponIndex = Math.Abs((currentWeaponIndex - 1) % weaponsAssociated.Count);
            SwitchWeapon();
        }
    }

    public void SwitchWeapon() {
        DisactivateRestOfWeapons(currentWeaponIndex);
        SetActiveWeapon(currentWeaponIndex);
    }

}
