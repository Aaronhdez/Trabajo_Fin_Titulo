using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public interface INode {
        NodeState Evaluate();
    }
}