using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : Stay{
	private WeaponHandling handling;
	public float accuracy = 0;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		handling = caster.GetComponent<WeaponHandling> ();
	}
	override public bool canPerform(GameObject target){
		return handling != null && handling.recoil > accuracy && base.canPerform(target);
	}
	override public void update(float dt){
		if (handling.recoil <= accuracy){
			complete();
		}
	}
	override public void onAnimation(int param = 0) {
	}
}