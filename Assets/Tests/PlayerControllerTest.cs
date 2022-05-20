using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace Tests.Character {
    public class PlayerControllerTest : MonoBehaviour {
        private GameObject player;
        private GameObject healthHUD;
        private PlayerController playerController;
        private HealthController healthController;

        [SetUp]
        public void SetUp() {
            player = Instantiate(
                Resources.Load<GameObject>("Prefabs/Characters/PlayerPrefab"));
            healthHUD = Instantiate(
                Resources.Load<GameObject>("Prefabs/HUD/HealthHud"));
            playerController = player.GetComponent<PlayerController>(); 
            healthController = player.GetComponentInChildren<HealthController>();
            healthController.healthText = healthHUD.GetComponentInChildren<TextMeshProUGUI>();
            playerController.healthController = healthController;
        }

        [Test]
        public void Player_is_locked_by_default() {
            Assert.IsTrue(playerController.IsLocked);
        }

        [Test]
        public void Player_is_unlocked_by_when_method_is_called() {
            playerController.Unlock();
            Assert.IsFalse(playerController.IsLocked);
        }

        [Test]
        public void Player_is_locked_by_when_method_is_called() {
            playerController.Unlock();
            playerController.Lock();
            Assert.IsTrue(playerController.IsLocked);
        }

        [Test]
        public void Health_is_restored_when_method_is_called() {
            healthController.currentHealth = 400;
            playerController.RestoreHealth();
            Assert.AreEqual(playerController.GetHealthValue(), 500);
        }

        [Test]
        public void Health_is_reduced_when_player_receives_damage() {
            playerController.ApplyDamage(15);
            Assert.AreEqual(playerController.GetHealthValue(), 485);
        }
    }
}