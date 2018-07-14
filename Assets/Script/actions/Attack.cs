using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action{
	private Weapon weapon;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
	}
	override public float range {
		get{ return weapon.range; }
	}
	override public void perform(GameObject trg){
		base.perform(trg);
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.SHOOT);
		//	weapon.startAttack();
	}
	// Any damage boosts go here
/*	protected Damage getDamage(){
		return Config.instance.damageFactory.getDamage(weapon, caster);
	}*/
	override public Action performPrepareAction(GameObject trg){
		Action act;
		if(weapon.clip == 0){
			act = new Reload();
			act.init(caster);
			return makePrepareAction(act,trg,trg);
		}
		return base.performPrepareAction(trg);
	}
	override public bool canPerform(GameObject target){
		Health th = target.GetComponent<Health> ();
		Unit cu = caster.GetComponentInParent<Unit> ();
		Unit tu = target.GetComponentInParent<Unit> ();
		Rotator rotator = caster.GetComponent<Rotator>();
		return (th.value > 0) && rotator != null && rotator.canTurnTo(target.transform.position) && ((cu.team & tu.team) == 0) && base.canPerform(target);	//Мертвых не бить! Своих тоже не бить
	}
/*	override public void onAnimation(int param = 0){
		switch(param){
			case Label.ATTACK:
				weapon.attack(target, getDamage());
				break;
			case Label.BURST:
				if(weapon.onBurst()){
					animator.playLabel("burst");
				}
				break;
			default:
				weapon.endAttack();
				complete();
		}
	}*/
	override public void update(float dt){
		Rotator rotator = caster.GetComponent<Rotator> ();
		if (rotator.turn (target.transform.position)) {
			weapon.shooting = true;
		}
	}
}