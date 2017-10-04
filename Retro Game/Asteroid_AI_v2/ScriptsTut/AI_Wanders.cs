using UnityEngine;
using System.Collections;

public class AI_Wanders : MonoBehaviour {

	Critter myCritter;

	Vector2 fromDir;
	Vector2 toDir;

	// Use this for initialization
	void Start () {
		myCritter = GetComponent<Critter>();

		fromDir = Random.onUnitSphere;
		toDir = Random.onUnitSphere;
	}


	void DoAIBehaviour() {
		Vector2 dir = fromDir;

		WeightedDirection wd = new WeightedDirection( dir, 0.01f );

		myCritter.desiredDirections.Add( wd );
	}

}
