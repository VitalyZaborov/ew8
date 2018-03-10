using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : Action{
	private const float SIGHT_CHECK_PERIOD = 1;

	private float sightCheckAt = 0;
	private Weapon weapon;
	public float accuracy = float.MaxValue;
	public int shots = int.MaxValue;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
	}
	override public float range {
		get{ return weapon.range; }
	}
	override public void perform(GameObject trg){
		base.perform(trg);
	//	weapon.startAttack();
	//	animator.playAnimation(weapon.anim, true);
	}
	// Any damage boosts go here
/*	protected Damage getDamage(){
		return Config.instance.damageFactory.getDamage(weapon, caster);
	}*/
/*	override public Action performPrepareAction(GameObject trg){
		Action act;
		if(weapon.clip == 0){
			act = new Reload();
			act.init(caster);
			return makePrepareAction(act,trg,trg);
		}
		return base.performPrepareAction(trg);
	}*/
	override public bool canPerform(GameObject target){
		Health th = target.GetComponent<Health> ();
		Unit cu = caster.GetComponent<Unit> ();
		Unit tu = target.GetComponent<Unit> ();
		Debug.Log("[Shoot]canPerform:" + (th.value > 0) +"|"+ ((cu.team & tu.team) == 0) + "|" + (weapon != null) + "|" + (weapon.clip > 0) +"|"+ (weapon.recoil <= accuracy));
		return (th.value > 0) && ((cu.team & tu.team) == 0) && (weapon != null) && (weapon.clip > 0) && (weapon.recoil <= accuracy) && base.canPerform(target);	//Мертвых не бить! Своих тоже не бить
	}
	override public void update(float dt) {

		Unit cu = caster.GetComponent<Unit> ();

		// Try to aim
		if (!cu.turn(target.transform.position)) {
			weapon.shooting = false;
			return;
		}

		// Stop shooting on empty clip, exceed recoil or burst size
		if (weapon.justShot && (weapon.clip == 0 || weapon.recoil > accuracy || weapon.burst > shots)) {
			complete();
			return;
		}

		// Spotting check
		if(Time.time - sightCheckAt > SIGHT_CHECK_PERIOD) {
			sightCheckAt = Time.time;
			if (!cu.canSee(target)){
				complete();
				return;
			}
		}

		if (!weapon.shooting){
			weapon.shooting = true;
		}
	}
	protected override void complete() {
		base.complete();
		weapon.shooting = false;
	}
}