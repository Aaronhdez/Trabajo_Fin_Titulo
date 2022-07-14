using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class TreeGenerator {
        Dictionary<string, ITree> treesAvaliable;

        public TreeGenerator(GameObject agent) {
            treesAvaliable = new Dictionary<string, ITree>();
            
            //V1
            treesAvaliable.Add("DummyBT_V1", new DummyBT_V1(null, agent));
            treesAvaliable.Add("BarkerBT_V1", new BarkerBT_V1(null, agent));
            
            //V2
            treesAvaliable.Add("DummyBT_V2", new DummyBT_V2(null, agent));
            treesAvaliable.Add("BarkerBT_V2", new BarkerBT_V2(null, agent));

            //V3
            treesAvaliable.Add("DummyBT_V3", new DummyBT_V3(null, agent));
            treesAvaliable.Add("BarkerBT_V3", new BarkerBT_V3(null, agent));
        }

        public ITree ConstructTreeFor(string treeToLoad) {
            return treesAvaliable[treeToLoad];
        }
    }
}