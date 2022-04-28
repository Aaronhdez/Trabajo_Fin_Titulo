using UnityEngine;

public class DeadState : MachineState
{
    private GameObject agent;
    private EnemyController enemyController;

    public DeadState(GameObject agent) {
        enemyController = agent.GetComponent<EnemyController>();
    }

    public void Enter() {
        Exit();
    }

    public void Exit() {
    }
}
