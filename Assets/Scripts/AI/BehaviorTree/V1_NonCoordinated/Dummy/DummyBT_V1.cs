using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class DummyBT_V1 : Tree {

        private INode _customRoot = null;

        public DummyBT_V1(INode root, GameObject agent) {
            _customRoot = root;
            _agent = agent;
        }

        protected override INode SetupTree() {
            if(_customRoot != null) {
                return _customRoot;
            }

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>(){
                    new CheckIfAgentIsDead(),
                    new Dead(_agent)
                }),
                new Sequence(new List<Node>() {
                    new CheckTargetIsInAttackRange(_agent),
                    new Attack(_agent)
                }),
                new Sequence(new List<Node>() { 
                    new CheckTargetIsInFOVRange(_agent),
                    new Chase(_agent)
                }),
                new WanderAround(_agent)
            });

            return root;
        }
    }
}