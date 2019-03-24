using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action {
	protected float follow_range;
	private Unit unit;
	private Vector3 prevPosition;
	protected UnityEngine.AI.NavMeshAgent nma;

	public MoveAction(float fr = 0) {
		follow_range = fr;
	}
	override public bool intercept() { return true; }
	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		unit = caster.GetComponent<Unit>();
		nma = caster.GetComponent<UnityEngine.AI.NavMeshAgent>();
		prevPosition = caster.transform.position;
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		nma.speed = unit.getSpeed();
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.RUN);
		update(0);
	}
	override public bool canPerform(GameObject target) {
		return unit.getSpeed() > 0;
	}
	override public void onAnimation(int param = 0) {
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.STAY);
		complete(); //Thinks every second when moves
	}
	override public void update(float dt) {
		Vector3 movement = (caster.transform.position - prevPosition).normalized;
		float forward = Vector3.Dot(movement, caster.transform.forward);
		float strafe = Vector3.Dot(movement, caster.transform.right);
		animator.SetFloat(Unit.FORWARD, forward);
		animator.SetFloat(Unit.STRAFE, strafe);
		prevPosition = caster.transform.position;
	}
}