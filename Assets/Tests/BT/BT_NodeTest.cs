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

        [Test]
        public void Default_state_returned_is_failure() {
            Node node = new Node();

            Assert.IsTrue(node.Evaluate()==NodeState.FAILURE);
        }
        
        [Test]
        public void Data_is_properly_set_up() {
            Node node = new Node();
            node.SetData("dummy", "dummy");

            Assert.IsNotNull(node.GetData("dummy"));
        }

        [Test]
        public void Data_can_be_accessed_in_parent_node() {
            Node node = new Node();
            List<Node> children = new List<Node>();
            children.Add(node);
            Node parent = new Node(children);
            parent.SetData("dummy", "dummy");

            Assert.IsNotNull(node.GetData("dummy"));
        }

        [Test]
        public void Data_can_be_cleared_in_current_node_if_exists() {
            Node node = new Node();
            node.SetData("dummy", "dummy");

            Assert.IsTrue(node.ClearData("dummy"));
        }

        [Test]
        public void Data_cannot_be_cleared_in_current_node_if_not_exists() {
            Node node = new Node();

            Assert.IsFalse(node.ClearData("dummy"));
        }
    }
}