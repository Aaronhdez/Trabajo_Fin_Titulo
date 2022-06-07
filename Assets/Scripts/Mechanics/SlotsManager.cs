using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics.Slots {
    public class SlotsManager {

        private readonly int slotsAvailable;
        private List<GameObject> slots;

        public SlotsManager(int slotsNumber) {
            this.slotsAvailable = slotsNumber;
        }

        public bool IsFull() {
            return (slots.Count() == slotsAvailable);
        }

        public void TakeSlot(GameObject gameObject) {
            slots.Add(gameObject);
        }

        public void Reset() {
            slots.Clear();
        }
    }
}