using BehaviorTree;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_DummyBT_V1_Test : MonoBehaviour {
    private DummyBT_V1 dummyBT;

    [Test]
    public void agent_should_attack_if_conditions_are_met() {
        var checkAttackRange = new Mock<CheckTargetIsInAttackRange>();
        var attack = new Mock<Attack>();
        var checkFOVRange = new Mock<CheckTargetIsInFOVRange>();
        var chase = new Mock<Chase>();
        var wanderAround = new Mock<WanderAround>();

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

        checkAttackRange.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
        attack.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

        dummyBT.Update();

        checkFOVRange.Verify(c => c.Evaluate(), Times.Never());
    }
}
