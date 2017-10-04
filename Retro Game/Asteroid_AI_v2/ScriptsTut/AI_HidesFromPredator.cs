using UnityEngine;
using System.Collections;

public class AI_HidesFromPredator : MonoBehaviour {

	public string critterType = "Carnivore";

	public float fearDistance = 5f;


	Critter myCritter;

	// Use this for initialization
	void Start () {
		myCritter = GetComponent<Critter>();
	}

	void DoAIBehaviour() {

		if(Critter.crittersByType.ContainsKey(critterType) == false) {
			// We have nothing to eat!
			return;
		}

		bool predatorNearby = false;

		foreach(Critter c in Critter.crittersByType[critterType]) {
			if(c.health <= 0) {
				// This is already dead, ignore it.
				continue;
			}

			float d = Vector2.Distance(this.transform.position, c.transform.position);

			if(d < fearDistance) {
				predatorNearby = true;
				break;
			}

		}

		if(predatorNearby == false) {
			// Nothing to fear.
			return;
		}


		// Now we want to move towards the closest hedge

		Hedge[] hedges = GameObject.FindObjectsOfType<Hedge>();
		Hedge closest = null;
		float dist = Mathf.Infinity;
		foreach(Hedge h in hedges) {
			float d = Vector2.Distance(this.transform.position, h.transform.position);
			if(closest == null || d < dist) {
				closest = h;
				dist = d;
			}
		}

		if(closest == null) {
			// no hedges!
			return;
		}
			

		Vector2 dir = closest.transform.position - this.transform.position;

		WeightedDirection wd = new WeightedDirection( dir, 10 );

		myCritter.desiredDirections.Add( wd );
	}

}
