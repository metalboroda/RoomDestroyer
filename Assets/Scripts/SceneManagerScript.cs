using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneManagerScript : MonoBehaviour {
    private void Start() {
        Application.targetFrameRate = 120;
    }

    public void ToGame() {
        SceneManager.LoadScene("Level1");
    }

    public void ToNextScene() {
        int randomLevel = Random.Range(1, 3);
        SceneManager.LoadScene(randomLevel);
    }
}