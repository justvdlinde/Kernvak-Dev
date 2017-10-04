using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public float speed;
	public string enemyType = "Player";

	static public Dictionary<string, List<Enemy>> enemyByType;
    public List<WeightedDirection> desiredDirections;
    
    Vector2 velocity;

	void Start () {       
		if(enemyByType == null) {
			enemyByType = new Dictionary<string, List<Enemy>>();
		}
		if(enemyByType.ContainsKey(enemyType) == false) {
			enemyByType[enemyType] = new List<Enemy>();
		}
		enemyByType[enemyType].Add(this);
	}

	void OnDestroy() {
		// Remove from the enemyByType list.
		enemyByType[enemyType].Remove(this);
	}
	
	void FixedUpdate () {
		desiredDirections = new List<WeightedDirection>();
		BroadcastMessage("DoAIBehaviour", SendMessageOptions.DontRequireReceiver);

		Vector2 dir = Vector2.zero;
		foreach(WeightedDirection wd in desiredDirections) {
			dir += wd.direction * wd.weight;
		}
		velocity = Vector2.Lerp(velocity, dir.normalized * speed, Time.deltaTime * 5f);
		transform.Translate( velocity * Time.deltaTime );
	}
}
