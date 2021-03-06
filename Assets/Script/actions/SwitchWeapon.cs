﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : Action {
	protected Soldier soldier;
	protected int switch_to;
	protected bool to_secondary;

	public SwitchWeapon(int to, bool to_secondary = false) {
		switch_to = to;
		this.to_secondary = to_secondary;
	}
	override public bool intercept() { return false; }
	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		soldier = caster.GetComponent<Soldier>();
	/*	switch_to %= soldier.weaponsArray.Length;
		if(switch_to < 0)
			switch_to = soldier.weaponsArray.Length + switch_to;*/

	}
	override public void perform(GameObject trg) {
		base.perform(trg);
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.SWITCH);
	}
	override public bool canPerform(GameObject target) {
		return soldier.weaponIndex != switch_to && soldier.weapons[switch_to] != null;
	}
	override public void onAnimation(int param = 0) {
		complete();
	}
	override protected void complete() {
		base.complete();
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.IDLE);
		soldier.weaponIndex = switch_to;
		if (to_secondary)
			soldier.switchSecondary();
	}
}