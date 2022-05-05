using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests {
    public class WeaponControllerTest : MonoBehaviour {
        [SetUp]

        [Test]
        public void Weapon_ammo_quantity_is_reduced_when_weapon_is_fired() {
            GameObject weapon = Instantiate(
                Resources.Load<GameObject>("Prefabs/Weapons/Rifle"));
            var weaponController = weapon.GetComponent<WeaponController>();
            var amountInMagazine = weaponController.amountInMagazine;

            weaponController.Fire();
            Assert.AreNotEqual(amountInMagazine, weaponController.amountInMagazine);
        }

        [Test]
        public void Weapon_ammo_quantity_is_restored_when_weapon_is_reloaded() {
            GameObject weapon = Instantiate(
                Resources.Load<GameObject>("Prefabs/Weapons/Rifle"));
            var weaponController = weapon.GetComponent<WeaponController>();
            var amountInMagazine = weaponController.amountInMagazine;

            weaponController.Fire();
            weaponController.Reload();
            Assert.AreEqual(amountInMagazine, weaponController.amountInMagazine);
        }
    }
}