using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : Action{
	private float follow_range;
	private bool always_follow;
	private UnityEngine.AI.NavMeshAgent nma;

	public Follow(bool af = true, float fr = 0){
		always_follow = af;
		follow_range = fr;
	}
	override public bool intercept(){return true;}
	override public void init(GameObject cst, object param = null){
		base.init(cst,param);
		nma = caster.GetComponent<UnityEngine.AI.NavMeshAgent> ();

	}
	override public void perform(GameObject trg){
		base.perform(trg);
		update (0);
	}
	override public bool canPerform(GameObject target) {
		return target != null && nma.speed > 0;
	}
	override public void onAnimation(int param = 0){
		complete();	//Thinks every second when moves
	}
	override public void update(float dt){
		caster.GetComponent<Unit>().turn(target.transform.position);
		float range = follow_range != 0 ? follow_range : nma.speed * dt;
		if(range != 0){
			if(Vector3.Distance(target.transform.position, caster.transform.position) <= (range + caster.GetComponent<SphereCollider>().radius + target.GetComponent<SphereCollider>().radius)){
				if(always_follow){
					animator.Play("stay");	//Стояние на месте - это тоже преследование
				}else{
					complete();
				}
			}else{
				animator.Play("run");
				nma.destination = target.transform.position;
			}
		}
	}
}