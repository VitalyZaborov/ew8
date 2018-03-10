using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinHP : TerminalCondition{
	
	override protected bool better(GameObject owner, GameObject player, GameObject best, GameObject unit){
		return unit.GetComponent<Health>().value < best.GetComponent<Health>().value;
	}
}