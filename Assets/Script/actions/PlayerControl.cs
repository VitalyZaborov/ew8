#define SPRINT_FOLLOWS_MOUSE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Action{
	
	private Weapon weapon;
	private Soldier soldier;
	private Unit unit;
	private Rigidbody rb;
	private Plane hPlane;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
		soldier = caster.GetComponent<Soldier>();
		rb = caster.GetComponent<Rigidbody>();
		unit = caster.GetComponent<Unit>();
		rb.freezeRotation = true;
		hPlane = new Plane(Vector3.up, Vector3.zero);
	}

	override public void update(float dt) {

		// Movement

		float axisX = Input.GetAxis("Horizontal");
		float axisY = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(axisX, 0, axisY);
		float dotProduct = Vector3.Dot(movement, caster.transform.rotation * Vector3.forward);
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (hPlane.Raycast(ray, out distance)) {
			Vector3 worldPos = ray.GetPoint(distance);
			caster.transform.LookAt(worldPos);
		}

#if SPRINT_FOLLOWS_MOUSE
		bool sprint = Input.GetButton("Sprint");
		float speed = unit.getSpeed(sprint);
		if(sprint) {
			movement = caster.transform.rotation * Vector3.forward * speed;
		} else {
			movement *= dotProduct >= 0 ? speed : speed * unit.backSpeedMod;
		}	
#else
		bool sprint = Input.GetButton("Sprint") && movement != Vector3.zero && dotProduct >= 0;

		float speed = unit.getSpeed(sprint);
		movement *= dotProduct >= 0 ? speed : speed * unit.backSpeedMod;
#endif
		rb.AddForce(movement, ForceMode.VelocityChange);

		animator.SetBool("sprint", sprint);

		// Shooting

		if (!sprint && Input.GetButtonDown("Fire1")) {
			weapon.shooting = true;
		}

		if (sprint || Input.GetButtonUp("Fire1")) {
			weapon.shooting = false;
		}

		if (Input.GetButtonDown("Reload")) {
			Action action = new Reload();
			action.init(caster);
			if(action.canPerform(null))
				brain.addOrder(action);
		}

		if (Input.GetButtonDown("Drop")) {
			soldier.dropWeapon();
			Action action = new SwitchEmptyWeapon();
			action.init(caster);
			if (action.canPerform(null))
				brain.addOrder(action);
		}

		if (Input.GetButtonUp("Fire3") && weapon.weapon != null) {
			soldier.switchSecondary();
		}

		float axisScroll = Input.GetAxis("Mouse ScrollWheel");
		if (axisScroll < 0) {
			if(weapon.weapon != null && weapon.weapon.secondary != null) {
				soldier.switchSecondary();
			} else {
				Action action = new SwitchWeapon(soldier.getNextWeaponIndex());
				action.init(caster);
				if (action.canPerform(null))
					brain.addOrder(action);
			}
			
		}
		else if (axisScroll > 0) {
			if (weapon.weapon != null && weapon.weapon == soldier.currentWeapon.secondary) {
				soldier.switchSecondary();
			} else {
				Action action = new SwitchWeapon(soldier.getPrevWeaponIndex(), true);
				action.init(caster);
				if (action.canPerform(null))
					brain.addOrder(action);
			}
				
		}
	}
	public override bool intercept() {
		return true;
	}
}