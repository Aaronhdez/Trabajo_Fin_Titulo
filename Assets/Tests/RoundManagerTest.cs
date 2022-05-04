using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class RoundManagerTest {

        private GameObject gameManager;
        private GameObject player;
        private RoundManager roundManager;
        private SpawnManager spawnManager;

        [SetUp]
        public void SetUp() {
            //Components must be assigned in setUp as if they wouldn't exists
            //Make'em public in objects
            gameManager = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("GameManager"));
            player = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("PlayerPrefab"));
            roundManager = gameManager.GetComponent<RoundManager>();
            roundManager.player = player;
            roundManager.playerController = player.GetComponent<PlayerController>();
        }

        [Test]
        public void RoundStatus_is_false_when_round_is_not_started() {;
            Assert.IsFalse(roundManager.roundStarted);
        }

        [Test]
        public void RoundStatus_is_true_when_round_starts() {
            roundManager.StartRound();
            Assert.IsTrue(roundManager.roundStarted);
        }

        [Test]
        public void Enemies_are_spawned_when_round_starts() {
            roundManager.StartRound();
            Assert.AreNotEqual(roundManager.enemiesAlive, 0);
        }

        [Test]
        public void Player_is_locked_before_round_starts() {
            Assert.IsFalse(roundManager.playerController.IsLocked);
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
    }
}