using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class HealthConsumableTest {
        private GameObject medKit;
        private GameObject player;
        private GameObject healthController;
        private HealthConsumable consumableController;
        private PlayerController playerController;

        [SetUp]
        public void SetUp(){
            medKit = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Supply/Consumables/MedKitProp"));
            player = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Characters/PlayerPrefab"));
            healthController = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Characters/PlayerAssets/HealthController"));
            consumableController = medKit.GetComponent<HealthConsumable>();
            playerController = player.GetComponent<PlayerController>();
            consumableController.player = player;
            playerController.healthController = healthController.GetComponent<HealthController>();
        }

        [Test]
        public void Player_health_is_restored_when_health_consumable_is_used() { 
            playerController.healthController.maxHealth = 100;
            playerController.healthController.currentHealth = 50;

            consumableController.ApplyEffectOver();
            Assert.AreEqual(playerController.healthController.maxHealth,
                playerController.healthController.currentHealth);
        }
    }
}