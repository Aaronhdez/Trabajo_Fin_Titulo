using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    //Environment Elements    
    [Header("Environment Elements")]
    [SerializeField] protected Transform target;
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected List<GameObject> patrolPath;

    //Machine Variables
    protected enum State {
        None,
        Patrol,
        Chase,
        Dead
    }
    protected State currentState;

    protected virtual void Initialize() { }
    protected virtual void StateUpdate() { }
    protected virtual void StateFixedUpdate() { }

    void Start() {
        Initialize();
    }

    void Update() {
        StateUpdate();
    }

    void FixedUpdate() {
        StateFixedUpdate();    
    }
}
