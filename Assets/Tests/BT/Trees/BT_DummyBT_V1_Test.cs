using BehaviorTree;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.V1 {
    public class BT_DummyBT_V1_Test : MonoBehaviour {
        private ITree dummyBT;
        private Mock<CheckTargetIsInAttackRange> checkAttackRange;
        private Mock<Attack> attack;
        private Mock<CheckTargetIsInFOVRange> checkFOVRange;
        private Mock<Chase> chase;
        private Mock<WanderAround> wanderAround;

        [SetUp]
        public void SetUp() {
            checkAttackRange = new Mock<CheckTargetIsInAttackRange>();
            attack = new Mock<Attack>();
            checkFOVRange = new Mock<CheckTargetIsInFOVRange>();
            chase = new Mock<Chase>();
            wanderAround = new Mock<WanderAround>();

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>() {
                    checkAttackRange.Object,
                    attack.Object
                }),
                new Sequence(new List<Node>() {
                    checkFOVRange.Object,
                    chase.Object
                }),
                wanderAround.Object
            });

            dummyBT = new DummyBT_V1(root);
        }

        [Test]
        public void Dummy_should_attack_if_conditions_are_met() {
            checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            attack.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            dummyBT.InitTree();
            dummyBT.UpdateNodes();

            checkAttackRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            attack.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange.Verify(c => c.Evaluate(), Times.Never());
        }

        [Test]
        public void Dummy_should_chase_if_attacking_is_not_possible() {
            checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkFOVRange.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            chase.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            dummyBT.InitTree();
            dummyBT.UpdateNodes();

            wanderAround.Verify(c => c.Evaluate(), Times.Never());
            checkAttackRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
        }

        [Test]
        public void Dummy_should_wander_if_the_rest_of_possibilites_are_not_possible() {
            checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            checkFOVRange.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);

            dummyBT.InitTree();
            dummyBT.UpdateNodes();

            checkAttackRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            checkFOVRange.Verify(c => c.Evaluate(), Times.AtLeastOnce());
            wanderAround.Verify(c => c.Evaluate(), Times.AtLeastOnce());
        }
    }
}