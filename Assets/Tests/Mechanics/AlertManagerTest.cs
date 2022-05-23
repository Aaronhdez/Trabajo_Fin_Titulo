using Mechanics;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tests.Mechanics {
    public class AlertManagerTest {

        [Test]
        public void Instance_should_return_AlertManager_value() {
            Assert.IsNotNull(AlertManager.GetInstance());
        }

        [Test]
        public void Alert_is_properly_triggered() {
            AlertManager.SetActiveStatusTo(true);
            Assert.IsTrue(AlertManager.AlertHasBeenTriggered());
        }

        [Test]
        public void Alert_is_properly_untriggered() {
            AlertManager.SetActiveStatusTo(false);
            Assert.IsFalse(AlertManager.AlertHasBeenTriggered());
        }
    }
}