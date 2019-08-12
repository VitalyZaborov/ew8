using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : UnitAction{
	
	override public void perform(GameObject trg){
		base.perform(trg);
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.ATTACK_1 + Random.Range(0, 3));
		//	weapon.startAttack();
	}
	// Any damage boosts go here
/*	protected Damage getDamage(){
		return Config.instance.damageFactory.getDamage(weapon, caster);
	}*/
	override public bool canPerform(GameObject target){
		Health th = target.GetComponent<Health> ();
		Unit cu = caster.GetComponentInParent<Unit> ();
		Unit tu = target.GetComponentInParent<Unit> ();
		return (th.value > 0) && ((cu.team & tu.team) == 0) && base.canPerform(target);	//Мертвых не бить! Своих тоже не бить
	}
	override public void onAnimation(int param = 0){
		switch(param){
			case Label.ATTACK:
				Damage damage = unit.getDamage();
				DamageDealer damager = unit.damageDealer;
				damager.deal(damage, range, target, unit.team);
				break;
//			case Label.BURST:
//				if(weapon.onBurst()){
//					animator.playLabel("burst");
//				}
//				break;
			default:
			//	weapon.endAttack();
				complete();
			break;
		}
	}
	override public void update(float dt){
		caster.transform.LookAt(target.transform.position);
	}
}