using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public class AlertManager {

        private static AlertManager _instance;
        private static bool status;

        public static AlertManager GetInstance() {
            if (_instance == null) {
                _instance = new AlertManager();
            }
            return _instance;
        }

        public static void SetActiveStatusTo(bool newStatus) {
            GetInstance();
           // _status = newStatus;
        }

        public static bool GetCurrentActiveStatus() {
            GetInstance();
            return status;
        }
    }
}