using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour {

    public GameObject Asteroid;
    public Transform target;

    void Update() {

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 0))
            {
                worldPos = hit.point;
            }
            else
            {
                worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            }
            Instantiate(Asteroid, worldPos, Quaternion.identity);
        }
    }
}
