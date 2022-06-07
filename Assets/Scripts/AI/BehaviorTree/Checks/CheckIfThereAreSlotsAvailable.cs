using Mechanics;
using Mechanics.Slots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfThereAreSlotsAvailable : Node {
        private readonly GameObject agent;

        public CheckIfThereAreSlotsAvailable() {
        }

        public CheckIfThereAreSlotsAvailable(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            ClearData("nextSlot");
            var lastAlertZone = AlertManager.GetLastZoneReportedInNodes();
            //Si el jugador puede ser perseguido directamente verificar y asignar


            //Si no puede, verificar slots en lugares adyacentes
            foreach (GameObject gameObject in lastAlertZone) {
                var slotsController = gameObject.GetComponent<SlotsController>();
                if (!slotsController.IsFull()) {
                    slotsController.TakeSlot(agent);
                    _parent.SetData("nextSlot", slotsController.transform.position);
                    state = NodeState.SUCCESS;
                    return state;
                }
            };

            state = NodeState.FAILURE;
            return state;
        }
    }
}