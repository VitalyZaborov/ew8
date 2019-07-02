using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Action {
	private static int animState = Animator.StringToHash("strike");
	private const float RANGE = 2.0f;
	private const float ANGLE = 30.0f;
	private Weapon weapon;

	override public void init(GameObject cst) {
		base.init(cst);
		weapon = caster.GetComponent<Weapon>();
	}
	override public float range {
		get { return RANGE; }
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.ATTACK_1);
	}

	override public bool canPerform(GameObject target) {
		if(target == null) {
			return base.canPerform(target);	// Всегда можно просто ударить воздух
		}
		Health th = target.GetComponent<Health>();
		Unit cu = caster.GetComponentInParent<Unit>();
		Unit tu = target.GetComponentInParent<Unit>();
		return (th.value > 0) && ((cu.team & tu.team) == 0) && base.canPerform(target);  //Мертвых не бить! Своих тоже не бить
	}

	override public void update(float dt) {
		caster.transform.LookAt(target.transform.position);
	}

	override public void onAnimation(int param = 0) {
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if (animState != info.shortNameHash)
			return;
		if(param == 1) {
			/*Weapon.WeaponData wdata = soldier.melee;
			Collider[] hitColliders = Physics.OverlapSphere(caster.transform.position, RANGE*10, 1, QueryTriggerInteraction.Ignore);
			foreach (Collider collider in hitColliders) {
				GameObject other = collider.gameObject;
				Unit unit = other.GetComponent<Unit>();
				Health health = other.GetComponent<Health>();
				if (unit != null && health != null && other != caster && Quaternion.Angle(caster.transform.rotation, Quaternion.LookRotation(other.transform.position - caster.transform.position)) <= ANGLE) {
					Damage damage = wdata.getDamage(caster);
					health.receiveDamage(damage.getDamageValue(0, 0));
				}
			}*/
		} else {
			complete();
		}
	}

	override protected void complete() {
		base.complete();
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.STAY);
	}
}