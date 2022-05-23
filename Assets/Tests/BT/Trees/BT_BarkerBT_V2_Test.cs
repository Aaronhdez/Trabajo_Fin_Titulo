using BehaviorTree;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.V2 {
    public class BT_BarkerBT_V2_Test {
        private ITree barkerBT;
        private Mock<CheckIfAgentIsDead> checkAgentIsDead;
        private Mock<Dead> dead;
        private Mock<CheckIfAlertIsTriggered> checkIfAlertIsTriggered;
        private Mock<CheckTargetIsInFOVRange> checkFOVRange_Alert;
        private Mock<CheckIfAlertIsNotTriggered> alert;
        private Mock<CheckIfAgentCanSpreadAlert> agentCanSpreadAlert;
        private Mock<SpreadAlert> spreadAlert;
        private Mock<CheckTargetIsInAttackRange> checkAttackRange;
        private Mock<Attack> attack;
        private Mock<CheckTargetIsInFOVRange> checkFOVRange_Chase;
        private Mock<Chase> chase;
        private Mock<WanderAround> wanderAround;

        [SetUp]
        public void SetUp() {
            checkAgentIsDead = new Mock<CheckIfAgentIsDead>();
            dead = new Mock<Dead>();
            checkIfAlertIsTriggered = new Mock<CheckIfAlertIsTriggered>();

            checkFOVRange_Alert = new Mock<CheckTargetIsInFOVRange>();
            alert = new Mock<CheckIfAlertIsNotTriggered>();
            agentCanSpreadAlert = new Mock<CheckIfAgentCanSpreadAlert>();
            spreadAlert = new Mock<SpreadAlert>();

            checkAttackRange = new Mock<CheckTargetIsInAttackRange>();
            attack = new Mock<Attack>();
            checkFOVRange_Chase = new Mock<CheckTargetIsInFOVRange>();
            chase = new Mock<Chase>();
            wanderAround = new Mock<WanderAround>();

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>() {
                    checkAgentIsDead.Object,
                    dead.Object
                }),
                new Sequence(new List<Node>(){
                    checkIfAlertIsTriggered.Object,
                    new Selector(new List<Node>() {
                        //Alertar
                        new Sequence(new List<Node>(){
                            checkFOVRange_Alert.Object,
                            alert.Object
                        }),
                        //Propagar Alerta
                        new Sequence(new List<Node>(){
                            agentCanSpreadAlert.Object,
                            spreadAlert.Object
                        })
                    })
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

            barkerBT = new BarkerBT_V2(root, null);
        }

        [Test]
        public void Barker_V2_should_die_if_the_agent_is_dead() {
            checkAgentIsDead.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            dead.Setup(c => c.Evaluate()).Returns(NodeState.RUNNING);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkAgentIsDead.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            dead.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkAttackRange.Verify(c => c.Evaluate(), Times.Never());
        }

        [Test]
        public void Barker_V2_should_alert_if_conditions_are_met() {
            checkAgentIsDead.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkIfAlertIsTriggered.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            checkFOVRange_Alert.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            alert.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkIfAlertIsTriggered.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange_Alert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            alert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkAttackRange.Verify(c => c.Evaluate(), Times.Never());
        }


        [Test]
        public void Barker_V2_should_spread_alert_if_conditions_are_met() {
            checkAgentIsDead.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkIfAlertIsTriggered.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            checkFOVRange_Alert.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            agentCanSpreadAlert.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            spreadAlert.Setup(c => c.Evaluate()).Returns(NodeState.RUNNING);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkIfAlertIsTriggered.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange_Alert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            alert.Verify(c => c.Evaluate(), Times.Never());
            agentCanSpreadAlert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            spreadAlert.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkAttackRange.Verify(c => c.Evaluate(), Times.Never());
        }

        [Test]
        public void Barker_V2_should_attack_if_conditions_are_met() {
            checkAgentIsDead.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkIfAlertIsTriggered.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            attack.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkIfAlertIsTriggered.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange_Alert.Verify(c => c.Evaluate(), Times.Never());
            alert.Verify(c => c.Evaluate(), Times.Never());
            checkAttackRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            attack.Verify(c => c.Evaluate(), Times.AtLeastOnce());
        }
        /*
        [Test]
        public void Barker_should_chase_if_conditions_are_met() {
            checkAgentIsDead.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkAlertNotTriggered.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkFOVRange_Chase.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            chase.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkAlertNotTriggered.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange_Alert.Verify(c => c.Evaluate(), Times.Never());
            alert.Verify(c => c.Evaluate(), Times.Never());
            checkAttackRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            attack.Verify(c => c.Evaluate(), Times.Never());
            checkFOVRange_Chase.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            chase.Verify(c => c.Evaluate(), Times.AtLeastOnce());
        }

        [Test]
        public void Barker_should_wander_if_conditions_are_met() {
            checkAgentIsDead.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkAlertNotTriggered.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkFOVRange_Chase.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            wanderAround.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            barkerBT.InitTree();
            barkerBT.UpdateNodes();

            checkAlertNotTriggered.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange_Alert.Verify(c => c.Evaluate(), Times.Never());
            alert.Verify(c => c.Evaluate(), Times.Never());
            checkAttackRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            attack.Verify(c => c.Evaluate(), Times.Never());
            checkFOVRange_Chase.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            chase.Verify(c => c.Evaluate(), Times.Never());
            wanderAround.Verify(c => c.Evaluate(), Times.AtLeastOnce());
        }*/
    }
}