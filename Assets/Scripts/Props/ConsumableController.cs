using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private bool playerInArea = false;
    [SerializeField] private GameObject player;
    [SerializeField] private string aspectToRestore;

    private void Start() {
        player = GameObject.Find("PlayerPrefab");
    }

    public void Update() {
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

    private void ApplyEffectOver() {
        player.GetComponent<PlayerController>().RestoreAspect(aspectToRestore);
    }
}
