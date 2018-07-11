using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action {
	protected float follow_range;
	protected bool run;
	private Unit unit;
	protected UnityEngine.AI.NavMeshAgent nma;

	public MoveAction(bool run = true, float fr = 0) {
		follow_range = fr;
		this.run = run;
	}
	override public bool intercept() { return true; }
	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		unit = caster.GetComponent<Unit>();
		nma = caster.GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		nma.speed = unit.getSpeed(run);
		animator.SetBool("sprint", run);
		update(0);
	}
	override public bool canPerform(GameObject target) {
		return unit.getSpeed() > 0;
	}
	override public void onAnimation(int param = 0) {
		animator.SetBool("sprint", false);
		complete(); //Thinks every second when moves
	}
}