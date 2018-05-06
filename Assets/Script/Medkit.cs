using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Pickup {

	public int value;

	override protected bool pickUp(GameObject o) {
		Health health = o.GetComponent<Health>();
		if (health != null && health.isAlive) {
			health.recover(value);
			return true;
		}
		return false;
	}
}
