using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    int getBullets = Gun.curAmmo;
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	public void OnCollisionEnter () {
        Destroy(gameObject);
        Gun.curAmmo += 1;
        getBullets += 1;

    }
    void OnGUI()
    {
        GUI.Box(new Rect(1500, 20, 250, 25), "Gun Ammunition = " + Gun.curAmmo);
    }
}
