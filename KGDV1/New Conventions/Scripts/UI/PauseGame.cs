using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = false;

        if (Input.GetKeyDown("escape"))
        {

            if (Time.timeScale == 1.0)
            {
                Time.timeScale = 0.00001f;
                Cursor.visible = true;
            }

            else
            {
                Time.timeScale = 1.0f;
                Cursor.visible = false;
            }

        }
    }
}
