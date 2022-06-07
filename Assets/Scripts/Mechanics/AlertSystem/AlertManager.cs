using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public class AlertManager {

        private static AlertManager _instance;
        private static bool status;
        private static Vector3 lastAlertPosition;
        private static List<Vector3> lastZoneReported;

        public static AlertManager GetInstance() {
            if (_instance == null) {
                _instance = new AlertManager();
            }
            return _instance;
        }

        public static void SetActiveStatusTo(bool newStatus) {
            GetInstance();
            status = newStatus;
        }

        public static bool AlertHasBeenTriggered() {
            GetInstance();
            return status;
        }

        public static Vector3 GetLastAlertPosition() {
            GetInstance();
            return lastAlertPosition;
        }

        public static void SetLastAlertPosition(Vector3 triggerPosition) {
            GetInstance();
            lastAlertPosition = triggerPosition;
        }

        public static void SetLastZoneReported(List<Vector3> lastZone) {
            lastZoneReported = lastZone;
        }

        public static List<Vector3> GetLastZoneReported() {
            return new List<Vector3>(lastZoneReported);
        }
    }
}