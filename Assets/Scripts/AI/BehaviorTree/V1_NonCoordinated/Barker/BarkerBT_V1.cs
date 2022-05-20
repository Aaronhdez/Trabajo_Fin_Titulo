using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class BarkerBT_V1 : Tree {

        public GameObject agent;
        private INode _customRoot = null;

        public BarkerBT_V1(INode root) {
            _customRoot = root;
        }

        protected override INode SetupTree() {
            if (_customRoot != null) {
                return _customRoot;
            }

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>(){

                }),
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