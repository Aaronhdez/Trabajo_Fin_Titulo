using System;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteStateMachine : FiniteStateMachine {

    public GameObject subject;
    public Dictionary<State, MachineState> behaviours;

    protected override void Initialize() {
        LoadBehavioursDictionary();    
    }

    private void LoadBehavioursDictionary() {
        behaviours = new Dictionary<State, MachineState>();
    }

    protected override void StateUpdate() { 
        
    }
    protected override void StateFixedUpdate() { 
    
    }
}
