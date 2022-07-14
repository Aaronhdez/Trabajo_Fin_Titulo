using Mechanics;
using NUnit.Framework;
using Moq;
using UnityEngine;
using BehaviorTree;

namespace Tests.BehaviourTrees.Nodes {
    public class BT_CheckIfAgentCanBeAlertedTest {
        private CheckIfAgentCanBeAlerted node;
        private GameObject dummyAgent;

        [SetUp]
        public void SetUp() {
            dummyAgent = new GameObject();
            dummyAgent.AddComponent(typeof(EnemyController_BT));
            dummyAgent.GetComponent<EnemyController_BT>().MaxDistanceToBeAlerted = 1f;
            node = new CheckIfAgentCanBeAlerted(dummyAgent);
        }

        [Test]
        public void Return_success_if_agent_is_in_distance_range() {
            dummyAgent.transform.position = new Vector3(3f, 2f, 0f);
            AlertManager.SetLastAlertPosition(new Vector3(3f, 1f, 0f));
            Assert.AreEqual(NodeState.SUCCESS, node.Evaluate());
        }

        [Test]
        public void Return_failure_if_agent_is_not_in_distance_range() {
            dummyAgent.transform.position = new Vector3(3f, 2f, 0f);
            AlertManager.SetLastAlertPosition(Vector3.positiveInfinity);

            Assert.AreEqual(NodeState.FAILURE, node.Evaluate());
        }
    }
}