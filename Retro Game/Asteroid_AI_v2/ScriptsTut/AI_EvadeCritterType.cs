using UnityEngine;
using System.Collections;

public class AI_EvadeCritterType : MonoBehaviour {

	public string critterType = "Carnivore";

	Critter myCritter;

	// Use this for initialization
	void Start () {
		myCritter = GetComponent<Critter>();
	}

	void DoAIBehaviour() {

		if(myCritter.isInHedge) {
			// We are hidden, so we don't have to evade anything.
			return;
		}



		if(Critter.crittersByType.ContainsKey(critterType) == false) {
			// We have nothing to eat!
			return;
		}

		// Find the closest edible critter to us.
		Critter closest = null;
		float dist = Mathf.Infinity;

		foreach(Critter c in Critter.crittersByType[critterType]) {
			if(c.health <= 0) {
				// This is already dead, ignore it.
				continue;
			}

			float d = Vector2.Distance(this.transform.position, c.transform.position);

			if(closest == null || d < dist) {
				closest = c;
				dist = d;
			}

		}

		if(closest == null) {
			// No valid food targets exist.
			return;
		}


		// Now we want to move towards this closest edible critter

		Vector2 dir = closest.transform.position - this.transform.position;
		dir *= -1;	// We are running AWAY from this.

		// IF the Badger is right on top of us, we'd like a weight of 100

		float weight = 10 / (dist * dist);

		WeightedDirection wd = new WeightedDirection( dir, weight );

		myCritter.desiredDirections.Add( wd );
	}
}
