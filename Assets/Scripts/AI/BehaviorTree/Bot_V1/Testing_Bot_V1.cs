using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class Testing_Bot_V1 : Tree {
        private INode _customRoot = null;

        public Testing_Bot_V1(INode root, GameObject agent) {
            _customRoot = root;
            _agent = agent;
        }

        protected override INode SetupTree() {
            if (_customRoot != null) {
                return _customRoot;
            }

            INode root = new Selector(new List<Node>() {
                new Sequence(new List<Node>(){
                    new CheckIfBotIsDead(_agent),
                    new BotDead(_agent)
                }),
                new FindEnemies(_agent)
            });

            return root;
        }
    }
}