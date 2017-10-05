using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform gunBullet;
    public Transform bulletSpawn;
    public Animation anim;
    public AudioSource aud;

    public static int curAmmo = 0;

    private RaycastHit hit;
    private Vector3 dir;
    private Quaternion rot;


    void Start()
    {
        anim = GetComponent<Animation>();
        aud = GetComponent<AudioSource>();
    }


    public void Update()
    {
        if (anim.isPlaying == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (curAmmo > 0)
                {
                    Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                    if (Physics.Raycast(ray, out hit))
                    {
                    dir = (hit.point - bulletSpawn.position).normalized;
                    }
                    else {
                    dir = ray.direction;
                    }
                    rot = Quaternion.FromToRotation(gunBullet.forward, dir);
                    Instantiate(gunBullet, bulletSpawn.position, bulletSpawn.rotation);
                    anim.Play("shoot");
                    aud.Play();
                    curAmmo -= 1;
                }
            }
        }
    }
}