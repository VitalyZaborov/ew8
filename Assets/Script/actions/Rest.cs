using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : Stay{
	private Weapon weapon;
	public float accuracy = 0;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
	}
	override public bool canPerform(GameObject target){
		return weapon != null && weapon.recoil > accuracy && base.canPerform(target);
	}
	override public void update(float dt){
		if (weapon.recoil <= accuracy){
			complete();
		}
	}
	override public void onAnimation(int param = 0) {
	}
}