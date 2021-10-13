using System;
using Player;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Managers {
    public class LevelManager : MonoBehaviour {
        
        // public static LevelManager Instance { get; private set; }

        public int targetScore;

        [SerializeField] private GameObject nextLevelPanel;
        [SerializeField] private GameObject input;

        // Delegate
        // public delegate void AddLevelCount(int levelCount);

        // public event AddLevelCount addLevelCount;
        // private int levelCountIncrease;

        /*private void Awake() {
            Instance = this;
        }*/

        void Start() {
            PropCollector.Instance.addScore += NextScene;
        }

        private void NextScene(int scoreCount) {
            if (scoreCount >= targetScore) {
                nextLevelPanel.SetActive(true);
                input.SetActive(false);
                // levelCountIncrease++;
                // addLevelCount?.Invoke(levelCountIncrease);
            }
        }
    }
}