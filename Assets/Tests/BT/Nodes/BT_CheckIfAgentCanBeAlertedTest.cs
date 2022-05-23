using Mechanics;
using NUnit.Framework;
using Moq;
using UnityEngine;
using BehaviorTree;

namespace Tests.BehaviourTrees.Nodes {
    public class BT_CheckIfAgentCanBeAlertedTest {

        [Test]
        public void Return_success_if_agent_is_in_distance_range() {
            GameObject dummyAgent = new GameObject();
            dummyAgent.transform.position = new Vector3(3f, 2f, 0f);
            AlertManager.SetLastAlertPosition(new Vector3(3f, 1f, 0f));
            var node = new CheckIfAgentCanBeAlerted(dummyAgent);

            Assert.AreEqual(NodeState.SUCCESS, node.Evaluate());
        }


        [Test]
        public void Return_failure_if_agent_is_not_in_distance_range() {
            GameObject dummyAgent = new GameObject();
            dummyAgent.transform.position = new Vector3(3f, 2f, 0f);
            AlertManager.SetLastAlertPosition(new Vector3(53f, 21f, 0f));
            var node = new CheckIfAgentCanBeAlerted(dummyAgent);

            Assert.AreEqual(NodeState.FAILURE, node.Evaluate());
        }
    }
}