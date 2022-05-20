using BehaviorTree;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests {
    public class BT_SequenceTest {
        private List<Mock<Node>> children2Node;

        [Test]
        public void Returns_failure_with_at_least_one_failure() {
            Mock<Node> child1 = new Mock<Node>();
            Mock<Node> child2 = new Mock<Node>();
            children2Node = new List<Mock<Node>> {
                child1,
                child2
            };

            List<Node> children = new List<Node>();
            children.Add(children2Node[0].Object);
            children.Add(children2Node[1].Object);
            Sequence selector = new Sequence(children);

            children2Node[0].Setup(c => c.Evaluate()).Returns(NodeState.SUCCESS);
            children2Node[1].Setup(c => c.Evaluate()).Returns(NodeState.FAILURE);

            Assert.AreEqual(NodeState.FAILURE, selector.Evaluate());
        }
    }
}