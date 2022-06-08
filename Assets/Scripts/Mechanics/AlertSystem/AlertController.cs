using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mechanics {
    public class AlertController : MonoBehaviour{
        public float _alertPeriod;
        private float _lastTriggertime = 0f;
        public bool _AlertTriggered;
        public bool furthestNodes;

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
                UpdatePositions();
            }
        }

        public void UpdatePositions() {
            AlertManager.ResetPositions();
            AlertManager.SetActiveStatusTo(true);
            AlertManager.SetLastAlertPosition(transform.position);
            TriangulatePlayerPosition(transform.position);
        }

        private void TriangulatePlayerPosition(Vector3 position) {
            var layerMask = (1 << 16);
            var alertPoints = Physics.OverlapSphere(position, 50f, layerMask);
            List<Collider> filteredPoints = GetOrderedPoints(position, alertPoints);
            UpdateZones(filteredPoints);
        }

        private List<Collider> GetOrderedPoints(Vector3 position, Collider[] alertPoints) {
            if (furthestNodes) {
                alertPoints.OrderByDescending(x => Vector3.Distance(x.transform.position, position));
            } else {
                alertPoints.OrderBy(x => Vector3.Distance(x.transform.position, position));
            }
            List<Collider> filteredPoints = new List<Collider>(alertPoints);
            return filteredPoints;
        }

        private static void UpdateZones(List<Collider> filteredPoints) {
            List<GameObject> lastZoneInNodes = new List<GameObject>();
            List<Vector3> lastZone = new List<Vector3>();

            foreach (var filteredPoint in filteredPoints.GetRange(0, 3)) {
                lastZone.Add(filteredPoint.transform.position);
                lastZoneInNodes.Add(filteredPoint.gameObject);
            }
            AlertManager.SetLastZoneReported(lastZone);
            AlertManager.SetLastZoneReportedInNode(lastZoneInNodes);
        }
    }
}