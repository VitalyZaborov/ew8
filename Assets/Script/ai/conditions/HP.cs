using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : NumericCondition{

	public HP(string sign, string param) : base (sign, param){
	}

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		Health health = unit.GetComponent<Health> ();
		return health ? compare (percent ? health.value / health.max * 100 : health.value) : false;
	}
}