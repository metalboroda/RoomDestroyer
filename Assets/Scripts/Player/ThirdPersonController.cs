using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player {
    public class ThirdPersonController : MonoBehaviour {
        // Input fields
        private PlayerInput playerActionsAsset;
        private InputAction move;

        // Movement fields
        private Rigidbody rb;
        [SerializeField] private float movementForce = 1f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float maxSpeed = 5f;
        private Vector3 forceDirection = Vector3.zero;

        [SerializeField] private Camera playerCamera;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            playerActionsAsset = new PlayerInput();
        }

        private void OnEnable() {
            playerActionsAsset.Player.Jump.started += DoJump;
            move = playerActionsAsset.Player.Move;
            playerActionsAsset.Player.Enable();
        }

        private void OnDisable() {
            playerActionsAsset.Player.Jump.started -= DoJump;
            playerActionsAsset.Player.Disable();
        }

        private void FixedUpdate() {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            if (rb.velocity.y < 0f) {
                rb.velocity += Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
            }

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;
            if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed) {
                rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
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
            }
        }

        private bool IsGrounded() {
            Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, 0.3f)) {
                return true;
            } else {
                return false;
            }
        }
    }
}