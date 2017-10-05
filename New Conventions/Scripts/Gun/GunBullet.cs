using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour {
    public int bulletSpeed = 1500;
    public Transform bulletSpawn;
    public Rigidbody rigidb;

    public AudioSource aud;

    public enemyHealth eh;

    private RaycastHit hit;
    private Vector3 dir;
    private Quaternion rot;


    void Start () {
        rigidb = GetComponent<Rigidbody>();
        bulletSpawn = GameObject.FindWithTag("BulletSpawn").transform;
        aud = GetComponent<AudioSource>();
    }
	

	void Update () {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        if (Physics.Raycast(ray, out hit)) {
            dir = (hit.point - bulletSpawn.position).normalized;
        }
        else {
            dir = ray.direction;
        }
        
        rigidb.AddForce(-bulletSpawn.forward * bulletSpeed);
	}

    void OnTriggerEnter (Collider other) {
    if (other.tag == "Enemy") {
            eh = (enemyHealth) other.GetComponent(typeof(enemyHealth));
            eh.ChangeHealth(-50);
            aud.Play();
        }
        Destroy(gameObject, 15f);
    }
}
