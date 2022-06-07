using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics.Slots {
    public class SlotsController {

        private int slotsNumber;
        private List<GameObject> slots;

        public int SlotsNumber { get => slotsNumber; set => slotsNumber = value; }

        public void Start() {
            slots = new List<GameObject>(slotsNumber);
        }

        public bool IsFull() {
            return (slots.Count() == slotsNumber);
        }
    }
}