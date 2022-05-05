using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests {
    public class WeaponControllerTest : MonoBehaviour {
        private GameObject weapon;
        private WeaponController weaponController;
        private int amountInMagazine;

        [SetUp]
        public void SetUp() {
            weapon = Instantiate(
                Resources.Load<GameObject>("Prefabs/Weapons/Rifle"));
            weaponController = weapon.GetComponent<WeaponController>();
        }

        [Test]
        public void Weapon_ammo_quantity_is_reduced_when_weapon_is_fired() {
            amountInMagazine = weaponController.amountInMagazine;
            weaponController.Fire();

            Assert.AreNotEqual(amountInMagazine, weaponController.amountInMagazine);
        }

        [Test]
        public void Weapon_ammo_quantity_is_restored_when_weapon_is_reloaded() {
            amountInMagazine = weaponController.amountInMagazine;
            weaponController.Fire();
            weaponController.Reload();

            Assert.AreEqual(amountInMagazine, weaponController.amountInMagazine);
        }

        [Test]
        public void Weapon_total_ammo_quantity_is_reduced_when_weapon_is_reloaded() {
            amountInMagazine = weaponController.amountOfBullets;
            weaponController.Fire();
            weaponController.Reload();

            Assert.AreNotEqual(amountInMagazine, weaponController.amountOfBullets);
        }

        [Test]
        public void Weapon_total_ammo_quantity_is_reduced_to_zero_on_last_reload() {
            weaponController.amountOfBullets = 30;
            weaponController.amountInMagazine = 10;
            weaponController.maxAmountInMagazine = 50;
            weaponController.Reload();

            Assert.AreEqual(weaponController.amountOfBullets, 0);
        }

        [Test]
        public void Weapon_total_ammo_quantity_is_restored_to_max_on_restoration() {
            weaponController.amountOfBullets = 30;
            weaponController.maxAmountOfBullets = 500;
            weaponController.RestoreAmmoValues();

            Assert.AreEqual(weaponController.amountOfBullets, weaponController.maxAmountOfBullets);
        }

        [Test]
        public void Weapon_total_ammo_quantity_in_magazine_is_restored_to_max_on_restoration() {
            weaponController.amountInMagazine = 25;
            weaponController.maxAmountInMagazine = 30;
            weaponController.RestoreAmmoValues();

            Assert.AreEqual(weaponController.amountInMagazine, weaponController.maxAmountInMagazine);
        }
    }
}