using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Action{
	
	private Weapon weapon;
	private Brain brain;
	private Plane hPlane;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		weapon = caster.GetComponent<Weapon> ();
		brain = caster.GetComponent<Brain> ();
		hPlane = new Plane(Vector3.up, Vector3.zero);
	}

	override public void update(float dt) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (hPlane.Raycast(ray, out distance)) {
			Vector3 worldPos = ray.GetPoint(distance);
			caster.transform.LookAt(worldPos);
		}

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
	}
	public override bool intercept() {
		return true;
	}
}