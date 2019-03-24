using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Action{
	
	private Unit unit;
	private Rigidbody rb;
	private Plane hPlane;
	private Action child;

	override public void init(GameObject cst,object param = null){
		base.init(cst, param);
		unit = caster.GetComponent<Unit>();
		hPlane = new Plane(Vector3.up, Vector3.zero);
	}

	override public void update(float dt) {
		
		// Aim

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (hPlane.Raycast(ray, out distance)) {
			Vector3 worldPos = ray.GetPoint(distance);
		}

		// Child action

		if (child != null) {
			child.update(dt);
			return;
		}
		
		// Shooting

		if (Input.GetButtonDown("Fire1")) {
			
		}

		if (Input.GetButtonDown("Strike") && addChildAction(new Strike())) {
			return;
		}

		if (Input.GetButtonDown("Reload") && addChildAction(new Reload())) {
			return;
		}
	}

	public override bool intercept() {
		return true;
	}

	private bool addChildAction(Action action) {
		action.init(caster);
		if (!action.canPerform(null)) {
			return false;
		}
		action.perform(null);
		child = action;
		child.evComplete += onActionComplete;
		return true;
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