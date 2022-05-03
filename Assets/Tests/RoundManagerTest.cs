using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class RoundManagerTest {

        private GameObject gameManager;
        private GameObject player;
        private RoundManager roundManager;

        [SetUp]
        public void SetUp() {
            gameManager = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("GameManager"));
            player = MonoBehaviour.Instantiate(
                Resources.Load<GameObject>("PlayerPrefab"));
            roundManager = gameManager.GetComponent<RoundManager>();
        }

        [Test]
        public void RoundStatus_is_false_when_round_is_not_started() {;
            Assert.IsFalse(roundManager.roundStarted);
        }

        [Test]
        public void RoundStatus_is_true_when_round_starts() {
            roundManager.StartRound();
            var status = roundManager.roundStarted;
            Assert.IsTrue(status);
        }

        [Test]
        public void Enemies_are_spawned_when_round_starts() {
            roundManager.StartRound();
            var enemiesSpawned = roundManager.enemiesAlive;
            Assert.AreNotEqual(enemiesSpawned, 0);
        }

        [Test]
        public void Player_is_locked_before_round_starts() {
            var playerStatus = player.GetComponent<PlayerController>().IsLocked;
            Assert.IsFalse(playerStatus);
        }

        [Test]
        public void Player_is_unlocked_after_round_starts() {
            var playerStatus = player.GetComponent<PlayerController>().IsLocked;
            roundManager.StartRound();
            Assert.IsFalse(playerStatus);
        }

    }
}