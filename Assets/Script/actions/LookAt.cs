using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : Action {
	private const float SIGHT_CHECK_PERIOD = 1;

	private float sightCheckAt = 0;

	override public float range {
		get { return float.MaxValue; }
	}
	override public void update(float dt) {
		Unit cu = caster.GetComponentInParent<Unit>();
		Rotator rotator = caster.GetComponent<Rotator>();
		rotator.turn(target.transform.position);

		if (Time.time - sightCheckAt > SIGHT_CHECK_PERIOD) {
			sightCheckAt = Time.time;
			if (cu.canSee(target)) {
				brain.memory.write("enemyPos", target.transform.position);
			} else {
				complete();
				return;
			}
		}
	}
}