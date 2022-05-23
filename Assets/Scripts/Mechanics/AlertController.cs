using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public class AlertController : MonoBehaviour {
        public float _alertPeriod;
        private float _lastTriggertime = 0f;

        public void Update() {
            var currentTriggertime = Time.time;
            if (AlertManager.AlertHasBeenTriggered()) {
                if (currentTriggertime - _lastTriggertime >= _alertPeriod) {
                    AlertManager.SetActiveStatusTo(false);
                    //borrar ultima posición
                }
            }
        }
        public void TriggerAlert() {
            var currentTriggertime = Time.time;
            if (!AlertManager.AlertHasBeenTriggered()) {
                _lastTriggertime = currentTriggertime;
                AlertManager.SetActiveStatusTo(true);
                //Actualizar ultima posición
            }
        }
    }
}