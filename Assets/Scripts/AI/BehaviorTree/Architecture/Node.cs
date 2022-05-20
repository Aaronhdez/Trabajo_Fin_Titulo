using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    
    public class Node : INode {
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
            foreach (Node child in children) {
                _Attach(child);
            }
        }

        private void _Attach(Node node) {
            node.parent = this;
            children.Add(node);
        }
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value) {
            _dataContext.Add(key, value);
        }

        public object GetData(string key) {
            //Intenamos obtener el valor en el nodo actual.
            object value = null;
            if (_dataContext.TryGetValue(key, out value)) {
                return value;
            }

            //Si no lo obtenemos, exploramos hacia arriba
            Node node = parent;
            while (node != null) {
                value = node.GetData(key);
                if (value != null) {
                    return value;
                }
                node = node.parent;
            }

            //Si no está, retornamos null
            return null;
        }

        public bool ClearData(string key) {
            //Intenamos borrar el valor en el nodo actual.
            if (_dataContext.ContainsKey(key)) {
                _dataContext.Remove(key);
                return true;
            }

            //Si no lo obtenemos, exploramos hacia arriba hasta borrarlo
            Node node = parent;
            while (node != null) {
                bool cleared = node.ClearData(key);
                if (cleared) {
                    return true;
                }
                node = node.parent;
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