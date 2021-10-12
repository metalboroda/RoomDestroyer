using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Managers {
    public class ScoreManagerScript : MonoBehaviour {
        [SerializeField] private Text scoreText;

        [SerializeField] private int targetScore;

        private void Start() {
            PropCollector.Instance.addScore += DisplayScore;
        }

        private void DisplayScore(int newCoins) {
            scoreText.text = $"{newCoins}/{targetScore}";
        }
    }
}