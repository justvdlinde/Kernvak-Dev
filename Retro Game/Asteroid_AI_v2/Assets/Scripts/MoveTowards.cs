using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour {
    public Transform target;
    public float speed;
    Vector3 dir;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        dir = target.position - transform.position;
    }

    void Update() {
        
        transform.position += dir * Time.deltaTime * speed;
    }
}
