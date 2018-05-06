using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private GameObject owner;
	private Vector3 movement;
	private float distance = 0;
	private float velocity = 0;
	private GameParams.GunParam gp;

	public void init (GameObject owner, float velocity) {
		this.owner = owner;
		this.velocity = velocity;
		movement = transform.rotation * Vector3.forward * velocity;
		gp = owner.GetComponent<Weapon>().gunParam;
	}

	void Update () {
		transform.position = transform.position + movement * Time.deltaTime;
		distance += velocity * Time.deltaTime;
	}

	void OnTriggerEnter (Collider other) {
		GameObject o = other.gameObject;
		if (other.isTrigger || other.tag == "Boundary" || other.tag == "Projectile" || o == owner)
			return;

		Health health = o.GetComponent<Health> ();
		if (health != null) {
			health.receiveDamage (Weapon.getDamage(gp, distance));
		}

		Destroy (gameObject);
	}
}
