using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Util {

	public static bool percent(float value){
		return Random.value * 100 < value;
	}
	public static float getRadius(GameObject o){
		if (o == null)
			return 0;
		Unit unit = o.GetComponent<Unit>();
		return unit != null ? unit.radius : 0;
	}
	public static uint getTeam(GameObject o){
		if (o == null)
			return 0;
		Unit unit = o.GetComponent<Unit>();
		return unit != null ? unit.team : 0;
	}
}
