using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public Text text;
    float timeLeft = 60.0f;

    void Update() {
        timeLeft -= Time.deltaTime;
        text.text = "Time Left: " + Mathf.Round(timeLeft);

        if (timeLeft < 0) {
            SaveScore();
            SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }
    }

    void SaveScore() {
        if(PlayerPrefs.GetInt("HighScore") < ScoreScript.scoreValue) {
            Debug.Log("Score Saved");
            PlayerPrefs.SetInt("HighScore", ScoreScript.scoreValue);
        }
    }
}
