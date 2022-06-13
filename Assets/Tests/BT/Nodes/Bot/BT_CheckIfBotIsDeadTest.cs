using BehaviorTree;
using NUnit.Framework;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes.Bot {
    public class BT_CheckIfBotIsDeadTest : MonoBehaviour {
        private GameObject dummyBot;

        [Test]
        public void Returns_true_if_bot_is_dead() {
            dummyBot = new GameObject();
            dummyBot.AddComponent(typeof(BotController));
            dummyBot.GetComponent<BotController>().IsDead = true;
            var node = new CheckIfBotIsDead(dummyBot);

            var deadStatus = node.Evaluate();

            Assert.AreEqual(NodeState.SUCCESS, deadStatus);
        }
    }
}