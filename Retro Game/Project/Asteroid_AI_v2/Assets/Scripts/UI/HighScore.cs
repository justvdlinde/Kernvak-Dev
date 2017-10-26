using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public Text yourScore;
    public Text highScore;

    private void Start() {
        yourScore.text = "Your Score: " + ScoreScript.scoreValue;
        highScore.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }
}
