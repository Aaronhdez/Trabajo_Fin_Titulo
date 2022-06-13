using BehaviorTree;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes.Bot {
    public class BT_CheckIfBotHealthIsLow {
        private GameObject dummyBot;

        [Test]
        public void Returns_true_if_bot_health_is_low() {
            dummyBot = new GameObject();
            dummyBot.AddComponent(typeof(BotController));
            dummyBot.GetComponent<BotController>().health = 10;
            var node = new CheckIfBotHealthIsLow(dummyBot);

            var deadStatus = node.Evaluate();

            Assert.AreEqual(NodeState.SUCCESS, deadStatus);
        }
    }
}