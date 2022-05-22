using BehaviorTree;
using Mechanics;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.Nodes {
    public class BT_CheckIfAlertIsNotTriggeredTest : MonoBehaviour {

        private CheckIfAlertIsNotTriggered node;

        [Test]
        public void Returns_success_if_alert_has_not_been_started() {
            AlertManager.SetActiveStatusTo(false);
            node = new CheckIfAlertIsNotTriggered();

            Assert.AreEqual(NodeState.SUCCESS, node.Evaluate());
        }
    }
}