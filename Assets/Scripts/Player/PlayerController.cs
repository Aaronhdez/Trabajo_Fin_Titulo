using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] public float speed;
    [SerializeField] public float cameraRotationSpeed;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController playerCC;
    [SerializeField] public Transform groundCheck;

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
    }

    // Update is called once per frame
    void LateUpdate() {
        ResetVelocityIfNeeded();
        Move();
        Jump();
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
}
