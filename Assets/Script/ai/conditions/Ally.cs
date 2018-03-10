﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : GroupCondition{

	override protected bool check(GameObject owner, GameObject player, GameObject unit){
		Unit me = owner.GetComponent<Unit> ();
		Unit he = unit.GetComponent<Unit> ();
		return me != null && he != null ? (me.team & he.team) != 0 : false;
	}
}