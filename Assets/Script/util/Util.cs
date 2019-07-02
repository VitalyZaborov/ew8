using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Util {

	public static bool percent(float value){
		return Random.value * 100 < value;
	}
}
