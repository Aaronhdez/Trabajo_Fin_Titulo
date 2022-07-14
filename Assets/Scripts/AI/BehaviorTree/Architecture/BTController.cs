using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class BTController : MonoBehaviour {
        [SerializeField] public string treeToLoad;
        [SerializeField] public GameObject agent;
        private ITree tree;

        public void Start() {
            tree = new TreeGenerator(agent).ConstructTreeFor(treeToLoad);
            tree.InitTree();
        }

        public void Update() {
            tree.UpdateNodes();
        }
    }
}