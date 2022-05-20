using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class BTController : MonoBehaviour {
        public ITree tree = null;
        public void Start() {
            tree.InitTree();
        }

        //Funci�n de evaluaci�n desde el nodo padre
        public void Update() {
            tree.UpdateNodes();
        }
    }
}