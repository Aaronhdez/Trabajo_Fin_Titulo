using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [Header("Player elements")]
    [SerializeField] public float speed;
    [SerializeField] public float cameraRotationSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController playerCC;
    [SerializeField] public Transform groundCheck;
    [SerializeField] private Dictionary<string, System.Action> actions;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> gunsAvailable;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private HealthController healthController;

    public float mass;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    public float gravity = -9.81f;
    public Vector3 velocity;
    public float jumpHeight = 2f;


    void Start() {
        mainCamera = GetComponentInChildren<Camera>();
        playerCC = GetComponent<CharacterController>();
        weaponController = weaponManager.CurrentWeapon;
        LoadActions();
    }

    private void LoadActions() {
        actions = new Dictionary<string, System.Action>();
        actions.Add(PlayerAspects.AMMO_KEY, RestoreAmmo);
        actions.Add(PlayerAspects.HEALTH_KEY, RestoreHealth);
    }

    void LateUpdate() {
        ResetVelocityIfNeeded();
        Move();
        Jump();
        Shoot();
        Reload();
        weaponController = weaponManager.CurrentWeapon;
    }

    private void Shoot() {
        if (Input.GetMouseButton(0)) {
            weaponController.Shoot();
        }
    }

    private void Reload() {
        if (Input.GetKeyDown(KeyCode.R)) {
            weaponController.Reload();
        }
    }

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        DoMovement(x, z);
    }

    private void DoMovement(float x, float z) {
        Vector3 moveVector = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;
        playerCC.Move(moveVector * speed * Time.deltaTime);
        playerCC.Move(velocity * Time.deltaTime);
    }

    private void ResetVelocityIfNeeded() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    public void RestoreAspect(string targetAspect) {
        actions[targetAspect]();
    }

    public void RestoreAmmo() {
        weaponController.ReplenishAmmo();
    }

    public void RestoreHealth() {
        healthController.RestorePlayerHealth();
    }
}
