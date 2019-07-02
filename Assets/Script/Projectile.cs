using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public bool explosive = false;
	public GameObject hitEffect;
	public float velocity = 10;
	public GameObject owner;

	private Vector3 startPosition;
	private Damage damage;

	public void init(GameObject owner, Damage damage) {
		this.owner = owner;
		this.damage = damage;
		startPosition = transform.position;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = transform.rotation * Vector3.forward * velocity;
	}

	public float distance {
		get {
			return (transform.position - startPosition).magnitude;
		}
	}

	private void OnTriggerEnter(Collider other) {
		
		GameObject o = other.gameObject;
		if (other.isTrigger || other.tag == "Boundary" || other.tag == "Projectile" || o == owner)
			return;
		hit(o);
	}

	private void hit(GameObject o) {
		if(!Statuses.processProjectile(this, damage, owner, o))
			return;

		if (hitEffect != null) {
			Instantiate(hitEffect, transform.position, transform.rotation);
		}
		
//		if (explosive) {
//			Collider[] objectsInRange = Physics.OverlapSphere(transform.position, damage.distMax);
//			foreach (Collider col in objectsInRange) {
//				GameObject target = col.gameObject;
//				Health health = target.GetComponent<Health>();
//				if (health != null) {
//					float proximity = (transform.position - target.transform.position).magnitude;
//					damage.value = (int)(damage.value * damage.decrease * proximity);
//					health.receiveDamage(damage.getDamageValue(crit, proximity));
//				}
//			}
//		} else {
//			Health health = o != null ? o.GetComponent<Health>() : null;
//			if (health != null) {
//				health.receiveDamage(damage.getDamageValue(crit, distance));
//			}
//		}
		Destroy(gameObject);
	}
}
