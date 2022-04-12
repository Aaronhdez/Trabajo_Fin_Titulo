using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConsumable : ConsumableController {
    
    protected string aspectToRestore = PlayerAspects.AMMO_KEY;

    protected override void ApplyEffectOver() {
        player.GetComponent<PlayerController>().RestoreAspect(aspectToRestore);
    }
}
