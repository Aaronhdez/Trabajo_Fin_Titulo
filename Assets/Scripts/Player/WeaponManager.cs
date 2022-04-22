using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [Header("Weapons Available")]
    [SerializeField] public List<GunController> gunsAssociated;
    [SerializeField] private GunController currentWeapon;

    private void Start() {
        SetUPWeapons();
    }

    private void SetUPWeapons() {
        gunsAssociated = new List<GunController>(GetComponentsInChildren<GunController>());
        currentWeapon = gunsAssociated[0];
        for (int index = 1; index < gunsAssociated.Count; index++) {
            gunsAssociated[index].SetInactive();
        }
        currentWeapon.SetActive();
    }

    public void SwitchGun(int index) { 

    }

}
