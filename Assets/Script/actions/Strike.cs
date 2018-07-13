using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Action {
	private static int animState = Animator.StringToHash("strike");
	private const float RANGE = 2.0f;
	private const float ANGLE = 30.0f;
	private const int DAMAGE = 100;
	private Weapon weapon;

	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon>();
	}
	override public float range {
		get { return RANGE; }
	}
	override public Action performPrepareAction(GameObject trg) {
		Action act;
		Rotator rotator = caster.GetComponent<Rotator>();
		if (Quaternion.Angle(caster.transform.rotation, Quaternion.LookRotation(trg.transform.position - caster.transform.position)) > ANGLE) {
			act = new TurnTo(ANGLE);
			act.init(caster);
			return makePrepareAction(act, trg, trg);
		}
		return base.performPrepareAction(trg);
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		animator.SetBool("strike", true);
	}

	override public bool canPerform(GameObject target) {
		if(target == null) {
			return base.canPerform(target);	// Всегда можно просто ударить воздух
		}
		Health th = target.GetComponent<Health>();
		Unit cu = caster.GetComponentInParent<Unit>();
		Unit tu = target.GetComponentInParent<Unit>();
		Rotator rotator = caster.GetComponent<Rotator>();
		return (th.value > 0) && rotator != null && rotator.canTurnTo(target.transform.position) && ((cu.team & tu.team) == 0) && base.canPerform(target);  //Мертвых не бить! Своих тоже не бить
	}

	override public void update(float dt) {
		if(target != null) {
			Rotator rotator = caster.GetComponent<Rotator>();
			rotator.turn(target.transform.position);
		}
	}

	override public void onAnimation(int param = 0) {
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if (animState != info.shortNameHash)
			return;
		if(param == 1) {
			Collider[] hitColliders = Physics.OverlapSphere(caster.transform.position, RANGE*10, 1, QueryTriggerInteraction.Ignore);
			foreach (Collider collider in hitColliders) {
				GameObject other = collider.gameObject;
				Unit unit = other.GetComponent<Unit>();
				Health health = other.GetComponent<Health>();
				if (unit != null && health != null && other != caster && Quaternion.Angle(caster.transform.rotation, Quaternion.LookRotation(other.transform.position - caster.transform.position)) <= ANGLE) {
					health.receiveDamage(DAMAGE);
				}
			}
		} else {
			complete();
		}
	}

	override protected void complete() {
		base.complete();
		animator.SetBool("strike", false);
	}
}