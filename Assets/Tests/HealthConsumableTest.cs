using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests {
    public class HealthConsumableTest {
        [Test]
        public void Player_health_is_restored_when_health_consumable_is_used() { 
            var medKit = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Supply/Consumables/MedKitProp"));
            var player = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Character/PlayerPrefab"));
            var consumableController = medKit.GetComponent<HealthConsumable>();
            var playerController = medKit.GetComponent<PlayerController>();
            playerController.healthController.maxHealth = 100;
            playerController.healthController.currentHealth = 50;
            consumableController.player = player;


            var newHealth = playerController.healthController.currentHealth;
            Assert.AreEqual(playerController.healthController.currentHealth, newHealth);
        }
    }
}