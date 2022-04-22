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
    [SerializeField] private Dictionary<string, System.Action> aspects;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> gunsAvailable;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GunController currentWeapon;

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
        aspects = new Dictionary<string, System.Action>();
        aspects.Add(PlayerAspects.AMMO_KEY, RestoreAmmo);
        aspects.Add(PlayerAspects.HEALTH_KEY, RestoreHealth);
        currentWeapon = weaponManager.CurrentWeapon;
    }

    // Update is called once per frame
    void LateUpdate() {
        ResetVelocityIfNeeded();
        Move();
        Jump();
        Shoot();
        Reload();
    }

    private void Shoot() {
        if (Input.GetMouseButton(0)) {
            currentWeapon.Shoot();
        }
    }

    private void Reload() {
        if (Input.GetKeyDown(KeyCode.R)) {
            currentWeapon.Reload();
        }
    }

    private void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        doMovement(x, z);
    }

    private void doMovement(float x, float z) {
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
        aspects[targetAspect]();
    }

    public void RestoreAmmo() {
        //gameManager.RestorePlayerAmmo();
        currentWeapon.ReplenishAmmo();
    }

    public void RestoreHealth() {
        gameManager.RestorePlayerHealth();
    }
}
