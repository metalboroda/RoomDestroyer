using System;
using UnityEngine;

namespace Player {
    public class PropDestroyer : MonoBehaviour {
        [SerializeField] private GameObject _particles;

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player")) {
                Instantiate(_particles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}