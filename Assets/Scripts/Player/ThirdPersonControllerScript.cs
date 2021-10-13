using System;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonControllerScript : MonoBehaviour {
    // Input fields
    private ThirdPersonActionsAsset playerActionsAsset;
    private InputAction move;

    // Particles field. Here because he's only one
    [SerializeField] private GameObject shockWaveParticles;

    // Components field
    [SerializeField] private CapsuleCollider propDestroyerCollider;

    // Movement fields
    private Rigidbody rb;
    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private Camera playerCamera;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        playerActionsAsset = new ThirdPersonActionsAsset();
        // capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void OnEnable() {
        playerActionsAsset.Player.Jump.started += DoJump;
        // playerActionsAsset.Player.ShockWave.started += DoShockWave;
        move = playerActionsAsset.Player.Move;
        playerActionsAsset.Player.Enable();
    }

    private void OnDisable() {
        playerActionsAsset.Player.Jump.started -= DoJump;
        // playerActionsAsset.Player.ShockWave.started -= DoShockWave;
        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate() {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0) {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed) {
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }

        LookAt();
    }

    private void LookAt() {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f) {
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        } else {
            rb.angularVelocity = Vector3.zero;
        }
    }

    private Vector3 GetCameraForward(Camera playerCamera1) {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera1) {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj) {
        if (IsGrounded()) {
            forceDirection += Vector3.up * jumpForce;
            movementForce = 0.25f;
            maxSpeed = 0.25f;
            ThirdPersonAnimation.Instance.animator.SetBool("jump", true);
        } else {
            ShockWaveMethod();
        }
    }

    /*private void DoShockWave(InputAction.CallbackContext obj) {
        if (!IsGrounded()) {
            forceDirection += Vector3.down * jumpForce;

            Instantiate(shockWaveParticles, transform.position, Quaternion.identity);
            // capsuleCollider.radius = 3.0f;
            propDestroyerCollider.enabled = true;
        }
    }*/

    private void ShockWaveMethod() {
        if (!IsGrounded()) {
            forceDirection += Vector3.down * jumpForce;

            Instantiate(shockWaveParticles, transform.position, Quaternion.identity);
            // capsuleCollider.radius = 3.0f;
            propDestroyerCollider.enabled = true;
        }
    }

    private bool IsGrounded() {
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f)) {
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Prop")) {
            // Move ability
            movementForce = 1f;
            maxSpeed = 5f;
            // Back to normal capsule radius
            // capsuleCollider.radius = 0.4f;
            propDestroyerCollider.enabled = false;
            ThirdPersonAnimation.Instance.animator.SetBool("jump", false);
        }
    }
}