using BehaviorTree;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_DummyBT_V1_Test : MonoBehaviour {
    private DummyBT_V1 dummyBT;

    [Test]
    public void agent_should_attack_if_conditions_are_met() {
        
        INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>() {
                    new CheckTargetIsInAttackRange(),
                    new Attack()
                }),
                new Sequence(new List<Node>() {
                    new CheckTargetIsInFOVRange(),
                    new Chase()
                }),
                new WanderAround()
            });
        dummyBT = new DummyBT_V1(null);
    }
}
