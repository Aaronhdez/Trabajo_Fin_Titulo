using BehaviorTree;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests {
    public class BT_NodeTest {
        [Test]
        public void Childen_list_is_updated_on_Node_assignment() {
            Node child = new Node();
            List<Node> children = new List<Node>();
            children.Add(child);
            Node parent = new Node(children);

            Assert.IsTrue(parent.children.ToArray().Length != 0);
        }
    }
}