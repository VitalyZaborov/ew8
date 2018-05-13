using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : NumericCondition{

	public Recoil(string sign, string param) : base (sign, param){
	}

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		WeaponHandling handling = unit.GetComponent<WeaponHandling> ();
		return handling ? compare (percent ? handling.recoil / handling.maxRecoil * 100 : handling.recoil) : false;
	}
}