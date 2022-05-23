using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class TreeGenerator {
        Dictionary<string, Tree> treesAvaliable;

        public TreeGenerator(GameObject agent) {
            treesAvaliable = new Dictionary<string, Tree>();
            treesAvaliable.Add("DummyBT_V1", new DummyBT_V1(null, agent));
            treesAvaliable.Add("DummyBT_V2", new DummyBT_V1(null, agent));
            treesAvaliable.Add("BarkerBT_V1", new BarkerBT_V1(null, agent));
        }

        public Tree ConstructTreeFor(string treeToLoad) {
            return treesAvaliable[treeToLoad];
        }
    }
}