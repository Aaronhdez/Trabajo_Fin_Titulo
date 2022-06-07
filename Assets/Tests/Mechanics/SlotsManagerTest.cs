using Mechanics.Slots;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Mechanics {
    public class SlotsManagerTest {

        [Test]
        public void Returns_not_full_if_slots_are_empty() {
            SlotsManager slotsController = new SlotsManager();
            slotsController.SlotsNumber = 5;
            slotsController.Start();
            var status = slotsController.IsFull();

            Assert.IsFalse(status);
        }

        [Test]
        public void Returns_full_if_slots_are_full() {
            SlotsManager slotsController = new SlotsManager();
            slotsController.SlotsNumber = 5;
            slotsController.Start();
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            var status = slotsController.IsFull();

            Assert.IsTrue(status);
        }
    }
}