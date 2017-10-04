using UnityEngine;
using System.Collections;

public class AI_EvadeEnemyType : MonoBehaviour {

	public string enemyType = "aiKiller";

	Enemy myEnemy;

	// Use this for initialization
	void Start () {
		myEnemy = GetComponent<Enemy>();
	}

	void DoAIBehaviour() {
        if (Enemy.enemyByType.ContainsKey(enemyType) == false)
        {
            // No asteroids spawned yet
            return;
        }

        Enemy closest = null;
		float dist = Mathf.Infinity;

		foreach(Enemy e in Enemy.enemyByType[enemyType]) {

			float d = Vector2.Distance(this.transform.position, e.transform.position);

			if(closest == null || d < dist) {
				closest = e;
				dist = d;
			}
		}

		if(closest == null) {
			return;
		}


        
        Vector2 dir = closest.transform.position - (this.transform.position / 2);
        //Vector2 angle = transform.forward; //Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        dir *= -1;

		float weight = 10 / (dist * dist);

		WeightedDirection wd = new WeightedDirection( dir, weight );

		myEnemy.desiredDirections.Add( wd );
	}
}
