using BehaviorTree;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace Tests {
    public class BT_SelectorTest {
        [Test]
        public void Selector_returns_failure_on_both_child_failure() {
            Mock<Node> child1 = new Mock<Node>();
            Mock<Node> child2 = new Mock<Node>();

            List<Node> children = new List<Node>();
            children.Add(child1.Object);
            children.Add(child2.Object);

            Selector selector = new Selector(children);

            child1.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            child1.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);

            Assert.AreEqual(NodeState.FAILURE, selector.Evaluate());
        }


        [Test]
        public void Selector_returns_success_on_one_child_success() {
            Mock<Node> child1 = new Mock<Node>();
            Mock<Node> child2 = new Mock<Node>();

            List<Node> children = new List<Node>();
            children.Add(child1.Object);
            children.Add(child2.Object);

            Selector selector = new Selector(children);

            child1.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            child1.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);

            Assert.AreEqual(NodeState.SUCCESS, selector.Evaluate());
        }

    }
}