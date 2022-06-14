using BehaviorTree;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes.Bot {
    public class BT_CheckIfThereAreBarkersNearbyTest : MonoBehaviour {
        private GameObject agent;
        private CheckIfThereAreBarkersNearby node;
        private GameObject dummyZombie;

        [SetUp]
        public void SetUp() {
            agent = new GameObject();
            agent.AddComponent<BotController>();
            agent.GetComponent<BotController>().FovRange = 10;
            agent.transform.position = new Vector3(0, 0, 0);
            node = new CheckIfThereAreBarkersNearby(agent);

            dummyZombie = new GameObject();
            dummyZombie.transform.position = new Vector3(0, 0, 12);
        }

        [Test]
        public void Returns_FAILURE_if_there_are_no_dummies_nearby() {
            var dummyZombieLayer = 14;
            dummyZombie.layer = dummyZombieLayer;

            Assert.AreEqual(NodeState.FAILURE, node.Evaluate());
        }
    }
}