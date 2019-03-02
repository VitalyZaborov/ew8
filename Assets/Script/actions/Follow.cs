using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MoveAction {
	private bool always_follow;

	public Follow(bool af = true, bool run = true, float follow_range = 0) : base(run, follow_range) {
		always_follow = af;
	}
	override public bool canPerform(GameObject target) {
		return target != null && base.canPerform(target);
	}
	override public void update(float dt){
		caster.GetComponent<Rotator>().turn(target.transform.position);
		float range = follow_range != 0 ? follow_range : nma.speed * dt;
		if(range != 0){
			if(Vector3.Distance(target.transform.position, caster.transform.position) <= (range + caster.GetComponent<UnityEngine.AI.NavMeshAgent>().radius + target.GetComponent<UnityEngine.AI.NavMeshAgent>().radius)){
				if(always_follow){
					if (animator != null)
						animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.IDLE);
				} else{
					complete();
				}
			}else{
				if(animator != null)
					animator.SetInteger(Unit.ANIMATION, (int)(run ? Unit.Animation.SPRINT : Unit.Animation.IDLE));
				nma.destination = target.transform.position;
			}
		}
		base.update(dt);
	}
}