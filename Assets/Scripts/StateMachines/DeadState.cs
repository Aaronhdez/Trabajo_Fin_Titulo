using UnityEngine;

public class DeadState : MachineState
{
    private GameObject subject;

    public DeadState(GameObject subject) {
        this.subject = subject;
    }

    public void Enter() {
        Destroy(subject);
    }

    public void Exit() {
        throw new System.NotImplementedException();
    }
}
