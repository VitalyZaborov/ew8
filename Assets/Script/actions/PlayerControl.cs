using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Action{
	
	private Unit unit;
	private Rigidbody rb;
	private Plane hPlane;
	private Action child;

	override public void init(GameObject cst){
		base.init(cst);
		unit = caster.GetComponent<Unit>();
		hPlane = new Plane(Vector3.up, Vector3.zero);
	}

	override public void update(float dt) {
		
		// Moving
        
        if (Input.GetButtonDown("ActionA")) {
        	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = 0;
            if (hPlane.Raycast(ray, out distance)) {
                Vector3 worldPos = ray.GetPoint(distance);
                if (addChildAction(new GoTo(worldPos))){
	                return;
                }
            }
        }
        
        // Child action

		if (child != null) {
			child.update(dt);
		}else{
			addChildAction(new Stay());
		}
		
//		if (Input.GetButtonDown("Strike") && addChildAction(new Strike())) {
//			return;
//		}
	}

	public override bool intercept() {
		return true;
	}

	private bool interceptChild(){
		if (child == null)
			return true;
		
		if (child.intercept()){
			child.evComplete -= onActionComplete;
			child = null;
			return true;
		}
		return false;
	}

	private bool addChildAction(Action action) {
		if (!interceptChild()){
			return false;
		}
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