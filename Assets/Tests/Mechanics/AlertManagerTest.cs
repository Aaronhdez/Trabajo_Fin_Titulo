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
    }
}