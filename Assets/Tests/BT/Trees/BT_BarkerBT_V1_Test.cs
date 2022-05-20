using BehaviorTree;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.V1 {
    public class BT_BarkerBT_V1_Test : MonoBehaviour {
        private ITree barkerBT;
        private Mock<CheckIfAlertIsNotTriggered> checkAlertNotTriggered;
        private Mock<CheckTargetIsInFOVRange> checkFOVRange_Alert;
        private Mock<CheckIfAlertIsNotTriggered> alert;
        private Mock<CheckTargetIsInAttackRange> checkAttackRange;
        private Mock<Attack> attack;
        private Mock<CheckTargetIsInFOVRange> checkFOVRange_Chase;
        private Mock<Chase> chase;
        private Mock<WanderAround> wanderAround;


        [Test]
        public void Barker_should_alert_if_conditions_are_met() {
            checkAlertNotTriggered = new Mock<CheckIfAlertIsNotTriggered>();
            checkFOVRange_Alert = new Mock<CheckTargetIsInFOVRange>();
            alert = new Mock<CheckIfAlertIsNotTriggered>();
            checkAttackRange = new Mock<CheckTargetIsInAttackRange>();
            attack = new Mock<Attack>();
            checkFOVRange_Chase = new Mock<CheckTargetIsInFOVRange>();
            chase = new Mock<Chase>();
            wanderAround = new Mock<WanderAround>();

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>(){ 
                    checkAlertNotTriggered.Object,
                    checkFOVRange_Alert.Object,
                    alert.Object
                }),
                new Sequence(new List<Node>() {
                    checkAttackRange.Object,
                    attack.Object
                }),
                new Sequence(new List<Node>() {
                    checkFOVRange_Chase.Object,
                    chase.Object
                }),
                wanderAround.Object
            });

            barkerBT = new BarkerBT_V1(root);

            checkAlertNotTriggered.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            checkFOVRange_Alert.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            alert.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkAlertNotTriggered.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange_Alert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            alert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkAttackRange.Verify(c => c.Evaluate(), Times.Never());
        }
    }
}