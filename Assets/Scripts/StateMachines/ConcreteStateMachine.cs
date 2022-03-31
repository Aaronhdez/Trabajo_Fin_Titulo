using System;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteStateMachine : FiniteStateMachine {

    public GameObject subject;
    private Dictionary<State, Action> behaviours;

    protected override void Initialize() {
        LoadBehavioursDictionary();    
    }

    private void LoadBehavioursDictionary() {
        behaviours = new Dictionary<State, Action>();
    }

    protected override void StateUpdate() { 
        
    }
    protected override void StateFixedUpdate() { 
    
    }


}
