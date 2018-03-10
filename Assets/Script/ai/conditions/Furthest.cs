using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furthest : TerminalCondition{
	
	override protected bool better(GameObject owner, GameObject player, GameObject best, GameObject unit){
		return Vector3.Distance (owner.transform.position, unit.transform.position) > Vector3.Distance (owner.transform.position, best.transform.position);
	}
}