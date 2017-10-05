using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour {

    public int currentHealth;
    public int maxHealth = 10;

    public float hpLenght;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        hpLenght = Screen.width / 3;
	}
	
	// Update is called once per frame
	void Update () {
        ChangeHealth(0);
    }

    public void ChangeHealth(int health) {
        currentHealth += health;
        hpLenght = (Screen.width / 2) * (currentHealth / (float)maxHealth);
        if(currentHealth <= 0) {
        Dead();

        }
    }

    void Dead()
    {
        Debug.Log("You Dieded");
        SceneManager.LoadScene("start");

    }


}
