using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : MoveAction {
	private Vector3 position;

	public GoTo(Vector3 pos, bool run = true, float follow_range = 0) : base(run, follow_range) {
		position = pos;
	}
	override public bool canPerform(GameObject target) {
		return position.sqrMagnitude != 0 && position != caster.transform.position && base.canPerform(target);
	}
	override public void update(float dt) {
		caster.GetComponent<Rotator>().turn(position);
		float range = follow_range != 0 ? follow_range : nma.speed * dt;
		if (range != 0) {
			if (Vector3.Distance(position, caster.transform.position) <= range) {
				complete();
			} else {
				animator.Play("run");
				nma.destination = position;
			}
		}
		base.update(dt);
	}
}