using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAction : Action {
	protected float follow_range;
	private Unit unit;
	private Vector3 prevPosition;
	protected UnityEngine.AI.NavMeshAgent nma;

	public MoveAction(float fr = 0) {
		follow_range = Mathf.Max(Mathf.Epsilon, fr);
	}

	override public bool intercept(){
		nma.isStopped = true;
		return true;
	}
	public override bool continious{	// Is it a long lasting action, which can be intercepted for AI checks
		get { return true; }
	}
	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		unit = caster.GetComponent<Unit>();
		nma = caster.GetComponent<UnityEngine.AI.NavMeshAgent>();
		prevPosition = caster.transform.position;
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		follow_range += getRadius(caster);
		nma.isStopped = false;
		nma.speed = unit.getSpeed();
		nma.stoppingDistance = follow_range;
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.RUN);
		update(0);
	}
	override public bool canPerform(GameObject target) {
		return unit.getSpeed() > 0;
	}
	override public void update(float dt) {
		if (nma.pathStatus == NavMeshPathStatus.PathComplete && nma.remainingDistance <= follow_range){
			complete();
		}
		Vector3 currentPosition = caster.transform.position;
		Vector3 movement = (currentPosition - prevPosition).normalized;
		float forward = Vector3.Dot(movement, caster.transform.forward);
		float strafe = Vector3.Dot(movement, caster.transform.right);
		animator.SetFloat(Unit.FORWARD, forward);
		animator.SetFloat(Unit.STRAFE, strafe);
		prevPosition = currentPosition;
	}

	override protected void complete(){
		intercept();
		base.complete();
	}

	protected static float getRadius(GameObject obj){
		NavMeshAgent nma = obj.GetComponent<NavMeshAgent>();
		return nma != null ? nma.radius : 0;
	}
}