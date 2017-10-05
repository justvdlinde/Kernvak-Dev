using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour {

    public Vector3 aimDownSight;
    public Vector3 hipFire;

	void Update () {
        if (Input.GetMouseButtonDown(1)) {
            transform.localPosition = aimDownSight;
        }

        if (Input.GetMouseButtonUp(1)) {
            transform.localPosition = hipFire;
        }
	}
}
