using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{

    [SerializeField] protected Animator animator;
    [SerializeField] protected bool playerInArea = false;
    [SerializeField] protected GameObject player;

    private void Start() {
        player = GameObject.Find("PlayerPrefab");
    }

    public void LateUpdate() {
        if (playerInArea) {
            if (Input.GetKeyDown(KeyCode.E)) {
                animator.Play("opened_closed");
                ApplyEffectOver();
                playerInArea = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerInArea = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerInArea = false;
        }
    }

    protected virtual void ApplyEffectOver() { }
}
