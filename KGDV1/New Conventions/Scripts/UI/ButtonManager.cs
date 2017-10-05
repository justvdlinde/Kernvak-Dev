﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void newGame(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
    }

    public void quitGame() {
        Application.Quit();
    }
}
