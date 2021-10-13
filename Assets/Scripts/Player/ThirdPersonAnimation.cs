using System;
using UnityEngine;

namespace Player {
    public class ThirdPersonAnimation : MonoBehaviour {
        public static ThirdPersonAnimation Instance { get; private set; }

        [HideInInspector] public Animator animator;
        private Rigidbody rb;
        private float maxSpeed = 5f;

        private static readonly int Speed = Animator.StringToHash("speed");

        private void Awake() {
            Instance = this;
        }

        void Start() {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            animator.SetFloat(Speed, rb.velocity.magnitude / maxSpeed);
        }
    }
}