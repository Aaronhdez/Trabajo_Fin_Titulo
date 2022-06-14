using BehaviorTree;
using NUnit.Framework;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes.Bot {
    public class BT_CheckIfThereAreDummiesNearby {
        private GameObject agent;
        private CheckIfThereAreDummiesNearby node;
        private GameObject dummyZombie;

        [SetUp]
        public void SetUp() {
            agent = new GameObject();
            agent.AddComponent<BotController>();
            agent.GetComponent<BotController>().FovRange = 10;
            agent.transform.position = new Vector3(0, 0, 0);
            node = new CheckIfThereAreDummiesNearby(agent);

            dummyZombie = new GameObject();
            dummyZombie.transform.position = new Vector3(0, 0, 12);
        }

        [Test]
        public void Returns_FAILURE_if_there_are_no_dummies_nearby() {
            var dummyZombieLayer = 13;
            dummyZombie.layer = dummyZombieLayer;

            Assert.AreEqual(NodeState.FAILURE, node.Evaluate());
        }

    }
}