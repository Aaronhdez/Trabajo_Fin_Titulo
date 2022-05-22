using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics {
    public class AlertManager {

        private static AlertManager _instance;

        public static AlertManager GetInstance() {
            if (_instance == null) {
                _instance = new AlertManager();
            }
            return _instance;
        }


    }
}