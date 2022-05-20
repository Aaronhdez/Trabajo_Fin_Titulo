using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class Tree : MonoBehaviour {
        private INode _rootNode = null;
        public void Start(){
            _rootNode = SetupTree();
        }

        //Función de evaluación desde el nodo padre
        public void Update() {
            if (_rootNode != null) {
                _rootNode.Evaluate();
            }
        }

        protected abstract INode SetupTree();
    }
}