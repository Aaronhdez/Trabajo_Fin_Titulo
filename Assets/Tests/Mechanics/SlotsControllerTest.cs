using Mechanics.Slots;
using NUnit.Framework;

namespace Tests.Mechanics {
    public class SlotsControllerTest {

        [Test]
        public void Returns_not_full_if_slots_are_empty() {
            SlotsController slotsController = new SlotsController();
            var status = slotsController.IsFull();

            Assert.IsFalse(status);
        }
    }
}