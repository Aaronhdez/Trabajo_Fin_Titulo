using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class DummyBT_V1 : Tree {

        public GameObject agent;

        protected override INode SetupTree() {
            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>() {
                    new CheckTargetIsInAttackRange(agent),
                    new Attack(agent)
                }),
                new Sequence(new List<Node>() { 
                    new CheckTargetIsInFOVRange(agent),
                    new Chase(agent)
                }),
                new WanderAround(agent)
            });

            return root;
        }
    }
}