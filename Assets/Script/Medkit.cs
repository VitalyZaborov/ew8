using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {

	public int value;

	void OnTriggerEnter (Collider other) {
		GameObject o = other.gameObject;
		if (other.tag == "Boundary" || other.tag == "Projectile")
			return;

		Health health = o.GetComponent<Health> ();
		if (health != null && health.isAlive) {
			health.recover (value);
			Destroy (gameObject);
		}

	}
}
