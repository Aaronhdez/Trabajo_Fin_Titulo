using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : ConsumableController {

    protected string aspectToRestore = PlayerAspects.HEALTH_KEY;

    public override void ApplyEffectOver() {
        player.GetComponent<PlayerController>().RestoreHealth();
    }
}
