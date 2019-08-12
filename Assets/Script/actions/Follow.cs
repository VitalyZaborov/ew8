using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MoveAction {

	public Follow(float fr = 0) : base(fr) {
	}
	
	public override bool canPerform(GameObject target) {
		return target != null && base.canPerform(target);
	}

	public override void perform(GameObject trg){
		follow_range += Util.getRadius(trg);
		base.perform(trg);
	}

	public override void update(float dt){
		base.update(dt);
		Vector3 targetPosition = target.transform.position;
		caster.transform.LookAt(targetPosition);
		nma.destination = targetPosition;
	}
}