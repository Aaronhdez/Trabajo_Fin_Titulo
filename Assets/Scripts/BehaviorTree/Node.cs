using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    
    public class Node {
        protected NodeState state;
        public List<Node> children = new List<Node>();
        public Node parent;
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();


        //Constructor vacío para el nodo padre del grafo
        public Node() {
            parent = null;
        }

        //Constructor vacío para el nodos hijos
        public Node(List<Node> children) {
        }

    }

    public enum NodeState {
        RUNNING,
        SUCCESS,
        FAILURE
    }


}