using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Action{
	
	private Weapon primary;
	private SecondaryWeapon secondary;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		primary = caster.GetComponent<Weapon> ();
		secondary = caster.GetComponent<SecondaryWeapon> ();
	}

	override public void update(float dt) {
		if (Input.GetButtonDown("Fire1")) {
			if (primary.clip > 0) {
				primary.shooting = true;
			} else {
				reload();
			}

		}

		if (Input.GetButtonUp("Fire1")) {
			primary.shooting = false;
		}
		if (Input.GetButtonDown("Fire2") && secondary.enabled) {
			if(secondary.clip > 0) {
				secondary.shooting = true;
			} else {
				reload();
			}
			Debug.Log("Fire2 "+ secondary.clip.ToString() + " " + secondary.shooting.ToString());
			
		}

		if (Input.GetButtonUp("Fire2") && secondary.enabled) {
			secondary.shooting = false;
		}

		if (Input.GetButtonDown("Reload")) {
			reload();
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
	private void reload() {
		Action action = new Reload();
		action.init(caster);
		if (action.canPerform(null))
			brain.addOrder(action);
	}
}