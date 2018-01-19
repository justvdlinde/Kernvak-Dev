using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour {

    public float maxDistance;
    public float cooldownTime;
    public playerHealth ph;
    public int damage;

    private Transform myTransform;
    public Transform target;

	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

        myTransform = transform;
        maxDistance = 10;
        cooldownTime = 0;
        damage = -10;

        ph = (playerHealth)go.GetComponent(typeof(playerHealth));
    }
	
	void Update () {
        float distance = Vector3.Distance(target.position, myTransform.position);
        if(distance < maxDistance) {
            Attack();
        }

        if(cooldownTime > 0) {
            cooldownTime = cooldownTime - Time.deltaTime;
        }

        if(cooldownTime < 0) {
            cooldownTime = 0;
        }
	}

    void Attack() {
        Vector3 dir = Vector3.Normalize(target.position - myTransform.position);
        float direction = Vector3.Dot(dir, transform.forward);
        if (direction > 0)
        {
            if (cooldownTime == 0)
            {
                ph.ChangeHealth(damage);
                cooldownTime = 2;
            }
        }
    }
}
