using BehaviorTree;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests {
    public class BT_SequenceTest {
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
        [TestCase(NodeState.FAILURE, NodeState.SUCCESS, NodeState.FAILURE)]
        [TestCase(NodeState.SUCCESS, NodeState.FAILURE, NodeState.FAILURE)]
        [TestCase(NodeState.FAILURE, NodeState.RUNNING, NodeState.FAILURE)]
        [TestCase(NodeState.RUNNING, NodeState.FAILURE, NodeState.FAILURE)]
        [TestCase(NodeState.RUNNING, NodeState.SUCCESS, NodeState.RUNNING)]
        [TestCase(NodeState.SUCCESS, NodeState.RUNNING, NodeState.RUNNING)]
        [TestCase(NodeState.SUCCESS, NodeState.SUCCESS, NodeState.SUCCESS)]
        public void Sequence_test_cases_for_two_nodes(NodeState state1, NodeState state2, NodeState result) {
            List<Node> children = new List<Node>();
            children.Add(children2Node[0].Object);
            children.Add(children2Node[1].Object);
            Sequence selector = new Sequence(children);

            children2Node[0].Setup(c => c.Evaluate()).Returns(state1);
            children2Node[1].Setup(c => c.Evaluate()).Returns(state2);

            Assert.AreEqual(result, selector.Evaluate());
        }
    }
}