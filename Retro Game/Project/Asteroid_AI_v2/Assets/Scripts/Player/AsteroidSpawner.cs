using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {
    public int pooledAmount = 5;
    public GameObject Asteroid;
    public Transform target;
    public AudioSource shoot;

    private List<GameObject> asteroids;

    private void Start()
    {
        asteroids = new List<GameObject>();
        for(int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(Asteroid);
            obj.SetActive(false);
            asteroids.Add(obj);
            AsteroidScript.numOfAsteroids += 1;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Fire();
        }
    }

    private void Fire() {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 worldPos;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0)) {
            worldPos = hit.point;
        }

        else {
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        }

        for (int i = 0; i <  asteroids.Count; i++ ) {
            if(!asteroids[i].activeInHierarchy) {
                asteroids[i].transform.position = worldPos;
                asteroids[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
                asteroids[i].SetActive(true);
                AudioSource shoot = GetComponent<AudioSource>();
                shoot.Play();
                break;
            }
        }
    }
}
    