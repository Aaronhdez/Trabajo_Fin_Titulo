using BehaviorTree;
using NUnit.Framework;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes.Bot {
    public class BT_CheckIfThereAreDummiesNearby {
        [Test]
        public void Returns_FAILURE_if_there_are_no_dummies_nearby() {
            var agent = new GameObject();
            agent.AddComponent<BotController>();
            agent.GetComponent<BotController>().FovRange = 10;
            agent.transform.position = new Vector3(0, 0, 0);
            var node = new CheckIfThereAreDummiesNearby(agent);

            var dummyZombie = new GameObject();
            var dummyZombieLayer = 13;
            dummyZombie.layer = dummyZombieLayer;
            dummyZombie.transform.position = new Vector3(0, 0, 12);

            Assert.AreEqual(NodeState.FAILURE, node.Evaluate());
        }


    }
}