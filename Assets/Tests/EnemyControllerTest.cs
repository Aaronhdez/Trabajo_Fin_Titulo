using NUnit.Framework;
using UnityEngine;

namespace Tests {
    public class EnemyControllerTest : MonoBehaviour {
        private GameObject enemy;
        private EnemyController enemyController;

        [SetUp]
        public void SetUp() {
            enemy = Instantiate(
                Resources.Load<GameObject>("Prefabs/Characters/Characters/Enemy1_V2"));
            enemyController = enemy.GetComponent<EnemyController>();
        }

        [Test]
        public void Enemy_health_is_decreased_if_damage_is_applied() {
            var health = enemyController.health;
            Assert.AreNotEqual(health, enemyController.health);
        }
    }
}