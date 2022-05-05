using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private RoundManager roundManager;

    void Start() {
        roundManager = GetComponent<RoundManager>();
    }

    void Update() {
    }
}
