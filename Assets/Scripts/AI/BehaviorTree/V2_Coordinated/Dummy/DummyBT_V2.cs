using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class DummyBT_V2 : Tree {

        private INode _customRoot = null;

        public DummyBT_V2(INode root, GameObject agent) {
            _customRoot = root;
            _agent = agent;
        }

        protected override INode SetupTree() {
            if (_customRoot != null) {
                return _customRoot;
            }

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>(){
                    new CheckIfAgentIsDead(_agent),
                    new Dead(_agent)
                }),
                new Sequence(new List<Node>() {
                    new CheckTargetIsInFOVRange(_agent),
                    new CheckTargetIsInAttackRange(_agent),
                    new Attack(_agent)
                }),
                new Selector(new List<Node>() {
                    new Sequence(new List<Node>() {
                        new CheckTargetIsInFOVRange(_agent),
                        new Chase(_agent)
                    }),
                    new Sequence(new List<Node>() {
                        new CheckIfAlertIsTriggered(_agent),
                        new CheckIfAgentCanBeAlerted(_agent),
                        new ChaseOnAlert(_agent)
                    }),
                }),
                new WanderAround(_agent)
            });

            return root;
        }
    }
}