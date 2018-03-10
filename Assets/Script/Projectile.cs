using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;

	private GameObject owner;
	private Vector3 movement;

	public void init (GameObject owner) {
		this.owner = owner;
		movement = transform.rotation * Vector3.forward * speed;
	}

	// Update is called once per frame
	void Update () {
		transform.position = transform.position + movement;
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
