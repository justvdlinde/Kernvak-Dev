using UnityEngine;
using System.Collections;

public class WeightedDirection {

    public readonly float weight;
    public readonly Vector2 direction;

	public WeightedDirection(Vector2 dir, float wgt) {
		direction = dir.normalized;
		weight = wgt;
	}
}
