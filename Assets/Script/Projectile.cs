using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private GameObject owner;
	private Vector3 movement;

	public void init (GameObject owner, float velocity) {
		this.owner = owner;
		movement = transform.rotation * Vector3.forward * velocity;
	}

	void Update () {
		transform.position = transform.position + movement * Time.deltaTime;
	}

	void OnTriggerEnter (Collider other) {
		GameObject o = other.gameObject;
		if (other.tag == "Boundary" || other.tag == "Projectile" || o == owner)
			return;

		Health health = o.GetComponent<Health> ();
		if (health != null) {
			health.receiveDamage (0);
		}

		Destroy (gameObject);
	}
}
