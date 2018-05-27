using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public bool explosive = false;
	public GameObject hitEffect;

	private GameObject owner;
	private Vector3 startPosition;
	private float velocity = 0;
	private float crit = 0;
	private Weapon.WeaponData wdata;

	public void init(GameObject owner, Weapon.WeaponData wdata, float crit = 0) {
		this.owner = owner;
		this.wdata = wdata;
		this.crit = crit;
		startPosition = transform.position;
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = transform.rotation * Vector3.forward * wdata.param.velocity;
	}

	private void OnTriggerEnter(Collider other) {
		
		GameObject o = other.gameObject;
		if (other.isTrigger || other.tag == "Boundary" || other.tag == "Projectile" || o == owner)
			return;

		hit(o);
	}

	private void hit(GameObject o) {
		if (hitEffect != null) {
			Instantiate(hitEffect, transform.position, transform.rotation);
		}

		if (explosive) {
			Collider[] objectsInRange = Physics.OverlapSphere(transform.position, wdata.param.distMax);
			foreach (Collider col in objectsInRange) {
				GameObject target = col.gameObject;
				Health health = target.GetComponent<Health>();
				if (health != null) {
					float proximity = (transform.position - target.transform.position).magnitude;
					health.receiveDamage(Weapon.getDamage(wdata.param, crit, proximity));
				}
			}
		} else {
			Health health = o != null ? o.GetComponent<Health>() : null;
			if (health != null) {
				health.receiveDamage(Weapon.getDamage(wdata.param, crit, (transform.position - startPosition).magnitude));
			}
		}
		Destroy(gameObject);
	}
}
