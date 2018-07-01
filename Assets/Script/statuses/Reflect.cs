using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : Status, IHitEffect {

	public bool onHit(Projectile projectile, Damage damage, GameObject attacker, GameObject target) {
		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		rb.velocity = rb.velocity * -1;
		return true;
	}
/*	override public void apply(GameObject gameObject) {
		owner = gameObject;
	}

	override public void remove() {
		owner = null;
	}

	override public void add(Status status) {

	}*/
}
