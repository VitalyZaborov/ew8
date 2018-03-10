using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : NumericCondition{

	public Ammo(string sign, string param) : base (sign, param){
	}

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		Weapon weapon = unit.GetComponent<Weapon> ();
		return weapon ? compare (percent ? weapon.ammo / weapon.maxAmmo * 100 : weapon.ammo) : false;
	}
}