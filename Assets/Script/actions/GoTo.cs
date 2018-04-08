using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : Action {
	private float follow_range;
	private Vector3 position;
	private UnityEngine.AI.NavMeshAgent nma;

	public GoTo(Vector3 pos, float fr = 0) {
		follow_range = fr;
		position = pos;
	}
	override public bool intercept() { return true; }
	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		nma = caster.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		update(0);
	}
	override public bool canPerform(GameObject target) {
		return position.sqrMagnitude != 0 && nma.speed > 0;
	}
	override public void onAnimation(int param = 0) {
		complete(); //Thinks every second when moves
	}
	override public void update(float dt) {
		caster.GetComponent<Unit>().turn(position);
		float range = follow_range != 0 ? follow_range : nma.speed * dt;
		if (range != 0) {
			if (Vector3.Distance(position, caster.transform.position) <= range) {
				complete();
			} else {
				animator.Play("run");
				nma.destination = position;
			}
		}
	}
}