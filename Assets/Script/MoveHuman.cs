using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHuman : MonoBehaviour {

	public float backSpeedMod = 1;

	private Rigidbody rb;
	private Plane hPlane;
	private UnityEngine.AI.NavMeshAgent nma;

	void Start () {
		nma = GetComponent<UnityEngine.AI.NavMeshAgent>();
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		hPlane = new Plane(Vector3.up, Vector3.zero);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0;
		if (hPlane.Raycast(ray, out distance)) {
			Vector3 worldPos = ray.GetPoint(distance);
			transform.LookAt(worldPos);
		}

		float speed = nma.speed;
		float axisX = Input.GetAxis ("Horizontal");
		float axisY = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (axisX, 0, axisY);
		float dotProduct = Vector3.Dot (movement, transform.rotation * Vector3.forward);
		movement *= (dotProduct >= 0 ? speed : speed * backSpeedMod);
		rb.velocity = movement;
	}
}
