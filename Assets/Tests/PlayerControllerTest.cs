using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests {
    public class PlayerControllerTest : MonoBehaviour {
        private GameObject player;
        private PlayerController playerController;

        [SetUp]
        public void SetUp() {
            player = Instantiate(
                Resources.Load<GameObject>("Prefabs/Characters/PlayerPrefab"));
            playerController = player.GetComponent<PlayerController>();
        }

        [Test]
        public void Player_is_locked_by_default() {
            Assert.IsTrue(playerController.IsLocked);
        }



    }
}