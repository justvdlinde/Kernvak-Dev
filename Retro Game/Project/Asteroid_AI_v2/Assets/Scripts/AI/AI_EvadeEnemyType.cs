using UnityEngine;
using System.Collections;

public class AI_EvadeEnemyType : MonoBehaviour {

	public string enemyType = "aiKiller";

    private Enemy myEnemy;

	private void Start () {
		myEnemy = GetComponent<Enemy>();
	}

	private void DoAIBehaviour() {
        if (Enemy.enemyByType.ContainsKey(enemyType) == false) {
            // No asteroids spawned yet
            return;
        }

        float dist = Mathf.Infinity;
        Enemy closest = null;

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

        float weight = 10 / (dist * dist);
        Vector2 dir = closest.transform.position - (this.transform.position / 2);
        dir *= -1;

		WeightedDirection wd = new WeightedDirection(dir, weight);
        myEnemy.desiredDirections.Add( wd );
    }
}
