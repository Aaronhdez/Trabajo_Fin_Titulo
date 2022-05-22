using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class Tree : ITree {

        public GameObject _agent;
        private INode _rootNode = null;

        public void InitTree() {
            _rootNode = SetupTree();
        }
        public void UpdateNodes() {
            if (_rootNode != null) {
                _rootNode.Evaluate();
            }
        }

        protected abstract INode SetupTree();
    }
}