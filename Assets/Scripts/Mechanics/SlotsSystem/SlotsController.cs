using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics.Slots {
    public class SlotsController : MonoBehaviour {
        private SlotsManager slotsManager;
        public int slotsAvailable;

        void Start() {
            slotsManager = new SlotsManager(slotsAvailable);
        }

        public void TakeSlot(GameObject enemy){
            slotsManager.TakeSlot(enemy);
        }

        public void ResetSlots() {
            slotsManager.Reset();
        }
    }
}