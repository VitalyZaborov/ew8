using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction : Action{
	protected Unit unit;
	
	override public void init(GameObject cst){
		base.init(cst);
		unit = caster.GetComponent<Unit> ();
	}
	override public float range {
		get{ return unit.range; }
	}
}