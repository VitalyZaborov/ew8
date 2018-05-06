using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		GameObject o = other.gameObject;
		if (other.tag == "Boundary" || other.tag == "Projectile")
			return;

		if (pickUp(o)) {
			Destroy(gameObject);
		}
	}

	virtual protected bool pickUp(GameObject o) {
		return false;
	}
}
