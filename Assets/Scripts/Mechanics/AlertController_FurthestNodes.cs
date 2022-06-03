using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics {
    public class AlertController_FurthestNodes : MonoBehaviour{
        public float _alertPeriod;
        private float _lastTriggertime = 0f;
        public bool _AlertTriggered;

        public void Update() {
            var currentTriggertime = Time.time;
            if (AlertManager.AlertHasBeenTriggered()) {
                if (currentTriggertime - _lastTriggertime >= _alertPeriod) {
                    AlertManager.SetActiveStatusTo(false);
                    AlertManager.SetLastAlertPosition(Vector3.positiveInfinity);
                }
            }
            _AlertTriggered = AlertManager.AlertHasBeenTriggered();
        }
        public void TriggerAlert() {
            var currentTriggertime = Time.time;
            if (!AlertManager.AlertHasBeenTriggered()) {
                _lastTriggertime = currentTriggertime;
                AlertManager.SetActiveStatusTo(true);
                AlertManager.SetLastAlertPosition(transform.position);
                AlertManager.SetLastZoneReported(TriangulatePlayerPosition(transform.position));
                Debug.Log("Ultima posición del juagdor: " + AlertManager.GetLastAlertPosition());
            }
        }

        private List<Vector3> TriangulatePlayerPosition(Vector3 position) {
            var layerMask = (1 << 16);
            var alertPoints = Physics.OverlapSphere(position, 50f, layerMask);
            alertPoints.OrderByDescending(x => Vector3.Distance(x.transform.position, position));
            List<Collider> filteredPoints = new List<Collider>(alertPoints);
            List<Vector3> lastZone = new List<Vector3>();
            foreach (var filteredPoint in filteredPoints.GetRange(0, 3)) {
                lastZone.Add(filteredPoint.transform.position);
            }
            return lastZone;
        }
    }
}