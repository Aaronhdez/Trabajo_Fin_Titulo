using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    
    public class Node : INode {
        protected NodeState state;
        public List<INode> _children = new List<INode>();
        public Node _parent;
        private Dictionary<string, object> _blackboard = new Dictionary<string, object>();


        //Constructor vacío para el nodo padre del grafo
        public Node() {
            _parent = null;
        }

        //Constructor vacío para el nodos hijos
        public Node(List<Node> children) {
            foreach (Node child in children) {
                _Attach(child);
            }
        }

        private void _Attach(Node node) {
            node._parent = this;
            _children.Add(node);
        }
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value) {
            _blackboard.Add(key, value);
        }

        public object GetData(string key) {
            //Intenamos obtener el valor en el nodo actual.
            object value = null;
            if (_blackboard.TryGetValue(key, out value)) {
                return value;
            }

            //Si no lo obtenemos, backtracking hacia arriba
            Node node = _parent;
            while (node != null) {
                value = node.GetData(key);
                if (value != null) {
                    return value;
                }
                node = node._parent;
            }

            //Si no está, retornamos null
            return null;
        }

        public bool ClearData(string key) {
            //Intenamos borrar el valor en el nodo actual.
            if (_blackboard.ContainsKey(key)) {
                _blackboard.Remove(key);
                return true;
            }

            //Si no lo obtenemos, backtracking hacia arriba hasta borrarlo
            Node node = _parent;
            while (node != null) {
                bool cleared = node.ClearData(key);
                if (cleared) {
                    return true;
                }
                node = node._parent;
            }

            //Si no está, retornamos falso
            return false;
        }
    }

    public enum NodeState {
        RUNNING,
        SUCCESS,
        FAILURE
    }

}