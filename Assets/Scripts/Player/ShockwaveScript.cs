using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Player {
    public class ShockwaveScript : MonoBehaviour {
        public static ShockwaveScript Instance { get; private set; }

        private CharacterController _characterController;
        [HideInInspector] public CapsuleCollider _capsuleCollider;
        [SerializeField] private GameObject shockWaveParticles;

        private void Awake() {
            Instance = this;

            _characterController = GetComponent<CharacterController>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ShockWaveMethod();
            }
        }

        public void ShockWaveMethod() {
            if (StarterAssets.ThirdPersonController.Instance.Grounded == false) {
                StarterAssets.ThirdPersonController.Instance.Gravity = -50.0f;
                _capsuleCollider.radius = 5.0f;
                var capsuleColliderCenter = _capsuleCollider.center;
                capsuleColliderCenter.y = 0.0f;
                Instantiate(shockWaveParticles, transform.position, Quaternion.identity);
                Debug.Log("ShockWave");
            }
        }

        public void BackToNormal() {
            _capsuleCollider.radius = 0.75f;
            var capsuleColliderCenter = _capsuleCollider.center;
            capsuleColliderCenter.y = 0.93f;
        }
    }
}