using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidScript : MonoBehaviour {

    public static int numOfAsteroids = 5;

    Text asteroids;

	void Start () {
        asteroids = GetComponent<Text>();
	}
	
	void Update () {
        asteroids.text = "Asteroids: " + numOfAsteroids;
	}
}
