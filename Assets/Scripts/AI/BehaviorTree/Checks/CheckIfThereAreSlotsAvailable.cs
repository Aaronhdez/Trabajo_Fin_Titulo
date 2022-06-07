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
            var lastAlertZone = AlertManager.GetLastZoneReportedInNodes();
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