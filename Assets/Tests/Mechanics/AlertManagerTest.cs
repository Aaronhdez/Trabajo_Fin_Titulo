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
            Assert.IsTrue(AlertManager.GetCurrentActiveStatus());
        }

        [Test]
        public void Alert_is_properly_un_triggered() {
            AlertManager.SetActiveStatusTo(false);
            Assert.IsFalse(AlertManager.GetCurrentActiveStatus());
        }
    }
}