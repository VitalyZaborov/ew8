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
	private Action child;

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
		
		// Aim

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (hPlane.Raycast(ray, out distance)) {
			Vector3 worldPos = ray.GetPoint(distance);
			caster.transform.LookAt(worldPos);
		}

		// Movement

		float axisX = Input.GetAxis("Horizontal");
		float axisY = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(axisX, 0, axisY);
		float dotProduct = Vector3.Dot(movement, caster.transform.rotation * Vector3.forward);

#if SPRINT_FOLLOWS_MOUSE
		bool sprint = Input.GetButton("Sprint") && child == null;
		float speed = unit.getSpeed(sprint);
		if(sprint) {
			movement = caster.transform.rotation * Vector3.forward * speed;
		} else {
			movement *= dotProduct >= 0 ? speed : speed * unit.backSpeedMod;
		}
#else
		bool sprint = Input.GetButton("Sprint") && child == null && movement != Vector3.zero && dotProduct >= 0;

		float speed = unit.getSpeed(sprint);
		movement *= dotProduct >= 0 ? speed : speed * unit.backSpeedMod;
#endif
		rb.AddForce(movement, ForceMode.VelocityChange);

		animator.SetBool("sprint", sprint);

		// Child action

		if(child != null) {
			child.update(dt);
			return;
		}
		
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
				addChildAction(action);
		}

		if (Input.GetButtonDown("Drop")) {
			soldier.dropWeapon();
			Action action = new SwitchEmptyWeapon();
			action.init(caster);
			if (action.canPerform(null))
				addChildAction(action);
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
					addChildAction(action);
			}
			
		}
		else if (axisScroll > 0) {
			if (weapon.weapon != null && weapon.weapon == soldier.currentWeapon.secondary) {
				soldier.switchSecondary();
			} else {
				Action action = new SwitchWeapon(soldier.getPrevWeaponIndex(), true);
				action.init(caster);
				if (action.canPerform(null))
					addChildAction(action);
			}
				
		}
	}

	public override bool intercept() {
		return true;
	}

	private void addChildAction(Action action) {
		if (!action.canPerform(null)) {
			return;
		}
		action.perform(null);
		child = action;
		child.evComplete += onActionComplete;
	}

	private void onActionComplete(Action action) {
		Debug.Assert(action == child, "Incorrect CHILD action completed! Current: " + (child != null ? child.id : "None") + " completed: " + action.id);
		child.evComplete -= onActionComplete;
		child = null;
	}
	override public void onAnimation(int param = 0) {
		if(child != null) {
			child.onAnimation(param);
		}
	}
}