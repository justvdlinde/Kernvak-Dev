using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroy : MonoBehaviour {

    private void OnEnable() {
        Invoke("Destroy", 5f);
        AsteroidScript.numOfAsteroids -= 1;
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void Destroy() {
        gameObject.SetActive(false);
        AsteroidScript.numOfAsteroids += 1;
    }
}
