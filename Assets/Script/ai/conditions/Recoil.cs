using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : NumericCondition{

	public Recoil(string sign, string param) : base (sign, param){
	}

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		Weapon weapon = unit.GetComponent<Weapon> ();
		return weapon ? compare (percent ? weapon.recoil / weapon.maxRecoil * 100 : weapon.recoil) : false;
	}
}