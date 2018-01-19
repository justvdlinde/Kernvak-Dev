using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {

    public int rotateSpeed = 5;
    public int movementSpeed = 7;
    public int maxDistance = 3;

    private Transform myTransform;
    public Transform target;


	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        myTransform = transform;
	}
	

	void Update () {
        moveTowardsPlayer();
	}

    void moveTowardsPlayer() {
        Debug.DrawLine(myTransform.position, target.position, Color.red);
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotateSpeed * Time.deltaTime);
        if (Vector3.Distance(target.position, myTransform.position) > maxDistance)
        {
            myTransform.position += myTransform.forward * movementSpeed * Time.deltaTime;
        }
    }
}
