using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 250f;

    public void Update() {
        var rotation = Time.deltaTime * rotationSpeed * Vector3.up;
        transform.Rotate(rotation);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            ApplyEffectOver(other);
        }
    }

    private void ApplyEffectOver(Collider other) {
        throw new NotImplementedException();
    }
}
