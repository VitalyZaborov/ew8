using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : MoveAction {
	private Vector3 position;

	public GoTo(Vector3 pos) : base(0) {
		position = pos;
	}
	
	override public bool canPerform(GameObject target) {
		// TODO: Fix sqrMagnitude hack!
		return position.sqrMagnitude != 0 && position != caster.transform.position && base.canPerform(target);
	}
	
	public override void perform(GameObject trg){
		nma.destination = position;
		base.perform(trg);
	}

	override public void update(float dt) {
		caster.transform.LookAt(position);
		base.update(dt);
	}
}