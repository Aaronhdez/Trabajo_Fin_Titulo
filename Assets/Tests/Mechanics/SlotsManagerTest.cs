using Mechanics.Slots;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Mechanics {
    public class SlotsManagerTest {

        private SlotsManager slotsController;

        [SetUp]
        public void SetUp() {
            slotsController = new SlotsManager(5);
            slotsController.Start();
        }


        [Test]
        public void Returns_not_full_if_slots_are_empty() {
            var status = slotsController.IsFull();

            Assert.IsFalse(status);
        }

        [Test]
        public void Returns_full_if_slots_are_full() {
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            var status = slotsController.IsFull();

            Assert.IsTrue(status);
        }

        [Test]
        public void Returns_not_full_if_slots_are_not_complete() {
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            slotsController.TakeSlot(new GameObject());
            var status = slotsController.IsFull();

            Assert.IsFalse(status);
        }
    }
}