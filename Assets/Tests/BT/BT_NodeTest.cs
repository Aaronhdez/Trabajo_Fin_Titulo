using BehaviorTree;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.BehaviourTrees.Architecture {
    public class BT_NodeTest {
        private Node node;

        [SetUp]
        public void SetUp() {
            node = new Node();
        }

        [Test]
        public void Childen_list_is_updated_on_Node_assignment() {
            List<Node> children = new List<Node>();
            children.Add(node);
            Node parent = new Node(children);

            Assert.IsTrue(parent.children.ToArray().Length != 0);
        }

        [Test]
        public void Default_state_returned_is_failure() {
            Assert.IsTrue(node.Evaluate()==NodeState.FAILURE);
        }
        
        [Test]
        public void Data_is_properly_set_up() {
            node.SetData("dummy", "dummy");

            Assert.IsNotNull(node.GetData("dummy"));
        }

        [Test]
        public void Data_can_be_accessed_in_parent_node() {
            List<Node> children = new List<Node>();
            children.Add(node);
            Node parent = new Node(children);
            parent.SetData("dummy", "dummy");

            Assert.IsNotNull(node.GetData("dummy"));
        }

        [Test]
        public void Data_can_be_cleared_in_current_node_if_exists() {
            node.SetData("dummy", "dummy");
            Assert.IsTrue(node.ClearData("dummy"));
        }

        [Test]
        public void Data_cannot_be_cleared_in_current_node_if_not_exists() {
            Assert.IsFalse(node.ClearData("dummy"));
        }

        [Test]
        public void Data_can_be_cleared_in_grandParent_node() {
            List<Node> children = new List<Node>();
            children.Add(node);
            Node parent = new Node(children);
            Node grandParent = new Node(children);
            grandParent.SetData("dummy", "dummy");

            Assert.IsTrue(node.ClearData("dummy"));
        }
    }
}