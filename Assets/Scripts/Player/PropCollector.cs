using System;
using UnityEngine;

namespace Player {
    public class PropCollector : MonoBehaviour {
        public static PropCollector Instance { get; private set; }

        public delegate void AddScore(int score);

        public event AddScore addScore;

        int scoreIncrease;

        private void Awake() {
            Instance = this;
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Prop")) {
                scoreIncrease++;
                addScore.Invoke(scoreIncrease);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Prop")) {
                scoreIncrease++;
                addScore.Invoke(scoreIncrease);
            }
        }
    }
}