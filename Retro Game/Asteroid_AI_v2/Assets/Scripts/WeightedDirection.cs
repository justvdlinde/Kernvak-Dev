using UnityEngine;
using System.Collections;

public class WeightedDirection {

	public readonly Vector2 direction;
	public readonly float weight;

	public WeightedDirection(Vector2 dir, float wgt) {
		direction = dir.normalized;
		weight = wgt;
	}
}
