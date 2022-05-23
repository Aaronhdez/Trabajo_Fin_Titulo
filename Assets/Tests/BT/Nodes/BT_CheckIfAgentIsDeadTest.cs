using BehaviorTree;
using Moq;
using NUnit.Framework;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes {
    public class BT_CheckIfAgentIsDeadTest {

        [Test]
        public void Returns_success_if_agent_is_dead(){
            GameObject dummyAgent = new GameObject();
            dummyAgent.AddComponent(typeof(EnemyController_BT));
            dummyAgent.GetComponent<EnemyController_BT>().IsDead = true;
            CheckIfAgentIsDead node = new CheckIfAgentIsDead(dummyAgent);

            Assert.AreEqual(NodeState.SUCCESS, node.Evaluate());
        }
    }
}