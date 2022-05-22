using Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class CheckIfAlertIsNotTriggered : Node {
        private readonly GameObject agent;
        private AlertController alertController;
        private readonly float _attackRange;

        public CheckIfAlertIsNotTriggered() {
        }

        public CheckIfAlertIsNotTriggered(GameObject agent) {
            this.agent = agent;
        }

        public override NodeState Evaluate() {
            //Verificar si el jugador ya ha alertado a otro barker
            //Si no se ha alertado, retornar exito
            if (!AlertManager.GetCurrentActiveStatus()) {
                state = NodeState.SUCCESS;
                return state;
            }

            //Ha dado la alerta retornar failure
            state = NodeState.FAILURE;
            return state;
            
        }

        private NodeState CheckIfPlayerIsOnAlertRange(object target) {
            return NodeState.FAILURE;
        }
    }
}