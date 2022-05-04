using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class RoundManagerTest {

        private GameObject gameManager;
        private GameObject player;
        private GameObject playerHUD;
        private GameObject enemiesHUD;
        private GameObject roundText;
        private RoundManager roundManager;
        private SpawnManager spawnManager;

        //Components must be assigned in setUp as if they wouldn't exists
        //Make'em public in objects implicated
        [SetUp]
        public void SetUp() {
            CreateGameObjects();
            roundManager.player = player;
            roundManager.playerController = player.GetComponent<PlayerController>();
            roundManager.playerHUD = playerHUD;
            roundManager.enemiesHUD = enemiesHUD;
            roundManager.roundText = roundText;
        }

        private void CreateGameObjects() {
            gameManager = MonoBehaviour.Instantiate(
                            Resources.Load<GameObject>("Prefabs/GameManager"));
            player = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/Characters/PlayerPrefab"));
            playerHUD = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/HUD/PlayerHud"));
            enemiesHUD = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("Prefabs/HUD/EnemiesHud"));
            roundText = MonoBehaviour.Instantiate(
                 Resources.Load<GameObject>("Prefabs/HUD/RoundText"));
            roundManager = gameManager.GetComponent<RoundManager>();
        }

        [Test]
        public void RoundStatus_is_false_when_round_is_not_started() {;
            Assert.IsFalse(roundManager.PlayingRound);
        }

        [Test]
        public void RoundStatus_is_true_when_round_starts() {
            roundManager.StartRound();
            Assert.IsTrue(roundManager.PlayingRound);
        }

        [Test]
        public void Enemies_are_spawned_when_round_starts() {
            roundManager.StartRound();
            Assert.AreNotEqual(roundManager.enemiesAlive, 0);
        }

        [Test]
        public void Player_is_locked_before_round_starts() {
            Assert.IsTrue(roundManager.playerController.IsLocked);
        }

        [Test]
        public void Player_is_unlocked_after_round_starts() {
            roundManager.StartRound();
            Assert.IsFalse(roundManager.playerController.IsLocked);
        }

        [Test]
        public void Player_is_locked_after_round_ends() {
            roundManager.EndRound();
            Assert.True(roundManager.playerController.IsLocked);
        }

        [Test]
        public void RoundsPlayed_varibales_is_incremented_on_round_ending() {
            roundManager.EndRound();
            Assert.AreEqual(roundManager.roundsPlayed, 1);
        }
    }
}