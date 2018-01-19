using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour {
    public float speed;
    public Transform target;

    private Vector3 dir;

    private void Update() {
        transform.position += dir * Time.deltaTime * speed;
    }

    private void OnEnable() {
        if (GameObject.FindWithTag("Player")) { 
            target = GameObject.FindWithTag("Player").transform;
            dir = target.position - transform.position;
        }
        else {
            Debug.Log("No Target Available");
        }
    }




}
