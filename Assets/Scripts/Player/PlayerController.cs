using Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerController {

    [Header("Player elements")]
    [SerializeField] private bool isLocked;
    [SerializeField] private float speed;
    [SerializeField] private float cameraRotationSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController playerCC;
    [SerializeField] private Transform groundCheck;
    [SerializeField] public Dictionary<string, System.Action> actions;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator playerAnimator;

    [Header("Player Props")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private List<GameObject> weaponsAvailable;
    [SerializeField] private WeaponController currentWeaponController;
    [SerializeField] public HealthController healthController;

    [Header("Physical Properties")]
    public float mass;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    [SerializeField] bool isGrounded;

    public float gravity = -9.81f;
    public Vector3 velocity;
    public float jumpHeight = 1.5f;

    [Header("Player Audio")]
    [SerializeField] private AudioSource footstep;

    public bool IsLocked { get => isLocked; set => isLocked = value; }

    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        mainCamera = GetComponentInChildren<Camera>();
        playerCC = GetComponent<CharacterController>();
        currentWeaponController = weaponManager.CurrentWeapon;
        healthController = GetComponentInChildren<HealthController>();
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool("ArmaCogida", true);
        LoadActions();
    }

    private void LoadActions() {
        actions = new Dictionary<string, System.Action>();
        actions.Add(PlayerAspects.AMMO_KEY, RestoreAmmo);
        actions.Add(PlayerAspects.HEALTH_KEY, RestoreHealth);
    }

    void LateUpdate() {
        ResetVelocityIfNeeded();
        if (!IsLocked) { 
            Move();
            Jump();
            Shoot();
            Reload();
            currentWeaponController = weaponManager.CurrentWeapon;
        }
        if (healthController.IsDead) {
            Lock();
            Cursor.lockState = CursorLockMode.None;
            gameManager.FinishGame();
        }
    }

    public void ResetAlertSystem() {
        gameObject.GetComponent<AlertController>().ResetAlertSystem();
    }

    private void ResetVelocityIfNeeded() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
    }

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        playerAnimator.SetFloat("VelX", x);
        playerAnimator.SetFloat("VelY", z);
        ProcessMovement(x, z);
    }

    private void ProcessMovement(float x, float z) {
        Vector3 moveVector = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;
        playerCC.Move(moveVector * speed * Time.deltaTime);
        playerCC.Move(velocity * Time.deltaTime);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }

    private void Shoot() {
        if (Input.GetMouseButton(0)) {
            currentWeaponController.Shoot();
        }
    }

    private void Reload() {
        if (Input.GetKeyDown(KeyCode.R)) {
            currentWeaponController.Reload();
        }
    }

    public void RestoreAspect(string targetAspect) {
        actions[targetAspect]();
    }

    public void RestoreAmmo() {
        currentWeaponController.ReplenishAmmo();
    }

    public void RestoreHealth() {
        healthController.RestorePlayerHealth();
    }
    
    public void ApplyDamage(int damageToApply) {
        healthController.DecreaseHealth(damageToApply);
    }

    public int GetHealthValue() {
        return healthController.currentHealth;
    }

    public void Lock() {
        mainCamera.GetComponent<CameraController>().enabled = false;
        playerCC.enabled = false;
        IsLocked = true;
    }

    public void Unlock() {
        mainCamera.GetComponent<CameraController>().enabled = true;
        playerCC.enabled = true;
        IsLocked = false;
    }
}
