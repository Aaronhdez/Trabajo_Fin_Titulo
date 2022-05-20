using BehaviorTree;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace Tests {
    public class BT_SelectorTest {

        private List<Mock<Node>> children2Node;
        private List<Mock<Node>> children3Node;

        [SetUp]
        public void Setup() {
            Mock<Node> child1 = new Mock<Node>();
            Mock<Node> child2 = new Mock<Node>();
            Mock<Node> child3 = new Mock<Node>();
            children2Node = new List<Mock<Node>> {
                child1,
                child2
            };
            children3Node = new List<Mock<Node>> {
                child1,
                child2,
                child3
            };
        }

        [TestCase(NodeState.FAILURE, NodeState.FAILURE, NodeState.FAILURE)]
        [TestCase(NodeState.FAILURE, NodeState.SUCCESS, NodeState.SUCCESS)]
        [TestCase(NodeState.SUCCESS, NodeState.FAILURE, NodeState.SUCCESS)]
        [TestCase(NodeState.FAILURE, NodeState.RUNNING, NodeState.RUNNING)]
        [TestCase(NodeState.RUNNING, NodeState.FAILURE, NodeState.RUNNING)]
        public void Selector_test_cases_for_two_nodes(NodeState state1, NodeState state2, NodeState result) {
            List<Node> children = new List<Node>();
            children.Add(children2Node[0].Object);
            children.Add(children2Node[1].Object);
            Selector selector = new Selector(children);

            children2Node[0].Setup(c => c.Evaluate()).Returns(state1);
            children2Node[1].Setup(c => c.Evaluate()).Returns(state2);

            Assert.AreEqual(result, selector.Evaluate());
        }

        [Test]
        public void Selector_returns_success_with_three_nodes() {
            Mock<Node> child1 = new Mock<Node>();
            Mock<Node> child2 = new Mock<Node>();
            Mock<Node> child3 = new Mock<Node>();

            List<Node> children = new List<Node>();
            children.Add(child1.Object);
            children.Add(child2.Object);

            Selector selector = new Selector(children);

            child1.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);
            child1.Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            child1.Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);

            Assert.AreEqual(NodeState.RUNNING, selector.Evaluate());
        }

    }
}