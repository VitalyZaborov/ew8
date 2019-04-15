using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MoveAction {
	private bool always_follow;

	public Follow(float fr = 0, bool af = true) : base(fr) {
		always_follow = af;
	}
	
	public override bool canPerform(GameObject target) {
		return target != null && base.canPerform(target);
	}

	public override void perform(GameObject trg){
		follow_range += getRadius(trg);
		base.perform(trg);
	}

	public override void update(float dt){
		Vector3 targetPosition = target.transform.position;
		caster.transform.LookAt(targetPosition);
		nma.destination = targetPosition;
		base.update(dt);
	}
}