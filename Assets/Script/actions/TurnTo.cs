using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTo : Action {
	private const float SIGHT_CHECK_PERIOD = 1;

	private float angle = 0.1f;

	public TurnTo() {
	}
	public TurnTo(float angle) {
		this.angle = angle;
	}
	override public float range {
		get { return float.MaxValue; }
	}

	override public bool canPerform(GameObject target) {
		Rotator rotator = caster.GetComponent<Rotator>();
		return rotator != null && rotator.canTurnTo(target.transform.position) && base.canPerform(target);
	}

	override public void update(float dt) {
		Unit cu = caster.GetComponentInParent<Unit>();
		Rotator rotator = caster.GetComponent<Rotator>();
		if(rotator.turn(target.transform.position, angle)) {
			complete();
		}
	}
}