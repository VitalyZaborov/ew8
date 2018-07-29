using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : Action {
	private static int animState = Animator.StringToHash("throw");
	private const float ANGLE = 5.0f;
	private Weapon.WeaponData grenade;

	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		Soldier soldier = caster.GetComponent<Soldier>();
		grenade = soldier.grenade;
	}
	override public float range {
		get { return grenade.param.range; }
	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.THROW);
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

	override public bool canPerform(GameObject target) {
		if(grenade.ammo == 0) {
			return false;
		}
		if (target == null) {
			return base.canPerform(target); // Всегда можно просто выкинуть гранату
		}
		Health th = target.GetComponent<Health>();
		Unit cu = caster.GetComponentInParent<Unit>();
		Unit tu = target.GetComponentInParent<Unit>();
		Rotator rotator = caster.GetComponent<Rotator>();
		return (th.value > 0) && rotator != null && rotator.canTurnTo(target.transform.position) && ((cu.team & tu.team) == 0) && base.canPerform(target);  //Мертвых не бить! Своих тоже не бить
	}

	override public void update(float dt) {
		if (target != null) {
			Rotator rotator = caster.GetComponent<Rotator>();
			rotator.turn(target.transform.position);
		}
	}

	override public void onAnimation(int param = 0) {
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if (animState != info.shortNameHash)
			return;
		if (param == 1) {
			Weapon weapon = caster.GetComponent<Weapon>();
			GameObject projectile = Resources.Load<GameObject>("projectiles/" + grenade.param.prj);
			GameObject o = Object.Instantiate(projectile, weapon.spawn.position, caster.transform.rotation);
			Projectile prj = o.GetComponent<Projectile>();

			prj.init(caster, grenade.getDamage(caster), grenade.param.velocity, 0);
		} else {
			complete();
		}
	}

	override protected void complete() {
		base.complete();
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.IDLE);
	}
}