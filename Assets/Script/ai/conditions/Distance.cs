using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : NumericCondition{

	public Distance(string sign, string param) : base (sign, param){
	}

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		float distance = Vector3.Distance (owner.transform.position, unit.transform.position);
		Weapon weapon = unit.GetComponent<Weapon> ();
		return percent ? compare (distance / weapon.range * 100) : compare (distance);
	}
}