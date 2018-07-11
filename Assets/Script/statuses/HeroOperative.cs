using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOperative : Status, IHitEffect, IDurationEffect {

	private const float STEALTH_TIMEOUT = 1.0f;
	private const float STEALTH_RANGE = 2.0f;

	private float idleTime;

	override public void apply(GameObject gameObject) {
		base.apply(gameObject);
	}

	override public void remove() {
		base.remove();
	}

	override public void add(Status status) {

	}

	public bool update(float dt) {

		return false;
	}

	public bool onHit(Projectile projectile, Damage damage, GameObject attacker, GameObject target) {
		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		rb.velocity = rb.velocity * -1;
		return false;
	}
}