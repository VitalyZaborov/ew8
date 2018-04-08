using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHuman : MonoBehaviour {

	public float backSpeedMod = 1;

	private Rigidbody rb;
	private UnityEngine.AI.NavMeshAgent nma;

	void Start () {
		nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float speed = nma.speed;
		float axisX = Input.GetAxis ("Horizontal");
		float axisY = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (axisX, 0, axisY);
		float dotProduct = Vector3.Dot (movement, transform.rotation * Vector3.forward);
		movement *= (dotProduct >= 0 ? speed : speed * backSpeedMod);
		rb.velocity = movement;
	}
}
