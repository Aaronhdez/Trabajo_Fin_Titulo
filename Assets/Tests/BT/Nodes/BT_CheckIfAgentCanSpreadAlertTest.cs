using BehaviorTree;
using Mechanics;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes {
    public class BT_CheckIfAgentCanSpreadAlertTest : MonoBehaviour {
        private CheckIfAgentCanSpreadAlert node;
        private GameObject dummyAgent;

        [SetUp]
        public void SetUp() {
            dummyAgent = new GameObject();
            node = new CheckIfAgentCanSpreadAlert(dummyAgent);
        }

        [Test]
        public void Return_success_if_agent_is_in_distance_range() {
            dummyAgent.transform.position = new Vector3(3f, 2f, 0f);
            AlertManager.SetLastAlertPosition(new Vector3(3f, 1f, 0f));
            Assert.AreEqual(NodeState.SUCCESS, node.Evaluate());
        }
    }
}