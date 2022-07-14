using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class BarkerBT_V3 : Tree {

        private INode _customRoot = null;

        public BarkerBT_V3(INode root, GameObject agent) {
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
                new Selector(new List<Node>() {
                    new Sequence(new List<Node>(){
                        new CheckIfAlertIsNotTriggered(_agent),
                        new CheckIfAgentCanSpreadAlert(_agent),
                        new CheckTargetIsInFOVRange(_agent),
                        new Alert(_agent)
                    }),
                    new Sequence(new List<Node>(){
                        new CheckIfAlertIsTriggered(_agent),
                        new Selector(new List<Node>() {
                            new Sequence(new List<Node>(){
                                new CheckTargetIsInFOVRange(_agent),
                                new Alert(_agent)
                            }),
                            new Sequence(new List<Node>(){
                                new CheckIfAgentCanSpreadAlert(_agent),
                                new SpreadAlert(_agent)
                            })
                        })
                    }),
                }),
                new Sequence(new List<Node>() {
                    new CheckTargetIsInFOVRange(_agent),
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