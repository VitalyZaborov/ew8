using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : Action{
	private static int animState = Animator.StringToHash("reload");
	private Weapon weapon;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
	}
	override public void perform(GameObject trg){
		base.perform(trg);
		if(weapon.weapon.param.reload == 0) {
			weapon.reload();
			complete();
			return;
		}
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.RELOAD);
		animator.speed = 1 / weapon.weapon.param.reload;
	}
	override public bool canPerform(GameObject target){
		return weapon.ammo > 0 && weapon.clip < weapon.maxClip;
	}
	override public void onAnimation(int param = 0){
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		if (animState != info.shortNameHash)
			return;
		weapon.reload();
		complete();
	}
	protected override void complete()
	{
		base.complete();
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.IDLE);
	}
}