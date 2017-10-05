using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 100;

    public float hpLenght;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        hpLenght = Screen.width / 3;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHealth(0);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 20, hpLenght, 25), "Ex Girlfriend Health = " + currentHealth + " / " + maxHealth);
        if (currentHealth <= 0)
        {
            print("dawdaw");
            GUI.Box(new Rect(Screen.width /2, Screen.height/2, 200, 200), "EYou won!!!");
        }
    }

    public void ChangeHealth(int health)
    {
        currentHealth += health;
        hpLenght = (Screen.width / 2) * (currentHealth / (float)maxHealth);

        if (currentHealth <= 0) {
            Dead();
        }
    }

    void Dead() {
        Destroy(gameObject);
    }
}
