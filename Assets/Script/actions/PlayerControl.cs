using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Action{
	
	private Weapon weapon;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
	}

	override public void update(float dt) {
		if (Input.GetButtonDown("Fire1")) {
			weapon.shooting = true;
		}

		if (Input.GetButtonUp("Fire1")) {
			weapon.shooting = false;
		}

		if (Input.GetButtonDown("Reload")) {
			Action action = new Reload();
			action.init(caster);
			if(action.canPerform(null))
				brain.addOrder(action);
		}

		if (Input.GetButtonDown("Drop")) {
			Soldier soldier = caster.GetComponent<Soldier>();
			soldier.dropWeapon();
			Action action = new SwitchEmptyWeapon();
			action.init(caster);
			if (action.canPerform(null))
				brain.addOrder(action);
		}
		
		float axisScroll = Input.GetAxis("Mouse ScrollWheel");
		if (axisScroll > 0) {
			Soldier soldier = caster.GetComponent<Soldier>();
			Action action = new SwitchWeapon(soldier.getNextWeaponIndex());
			action.init(caster);
			if (action.canPerform(null))
				brain.addOrder(action);
		}
		if (axisScroll > 0) {
			Soldier soldier = caster.GetComponent<Soldier>();
			Action action = new SwitchWeapon(soldier.getPrevWeaponIndex());
			action.init(caster);
			if (action.canPerform(null))
				brain.addOrder(action);
		}
	}
	public override bool intercept() {
		return true;
	}
}