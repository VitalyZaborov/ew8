using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : NumericCondition{

	public Clip(string sign, string param) : base (sign, param){
	}

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		Weapon weapon = unit.GetComponent<Weapon> ();
		return weapon != null ? compare (percent ? weapon.clip / weapon.maxClip * 100 : weapon.clip) : false;
	}
}