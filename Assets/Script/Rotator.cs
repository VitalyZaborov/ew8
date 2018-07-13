using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rotator : MonoBehaviour {
	public float angle = 180;
	public float angularSpeed = -1;

	private Quaternion startRotation;

	public void Awake() {
		startRotation = transform.rotation;
		if(angularSpeed < 0) {
			NavMeshAgent nma = GetComponent<NavMeshAgent>();
			angularSpeed = nma.angularSpeed;
		}
	}

	public bool canTurnTo(Vector3 position) {
		return angle >= 180 || Quaternion.Angle(transform.rotation, Quaternion.LookRotation(position - transform.position)) < angle;
	}
	public bool turn(Vector3 position, float angle = 0.1f) {
		
		Quaternion targetRotation = Quaternion.LookRotation(position - transform.position);
		float step = angularSpeed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
		return Quaternion.Angle(transform.rotation, targetRotation) < angle;
		/*	float angle = Vector3.Angle(transform.position, position);
			if (angle == 0)
				return true;
			NavMeshAgent nma = GetComponent<NavMeshAgent>();
			Debug.Log("Turn: " + angle.ToString() + transform.position.ToString() + " -> " + position.ToString());
			angle = Mathf.Min(angle, Time.deltaTime * nma.angularSpeed);
			transform.Rotate(Vector3.up, angle);*/
		/*	bool updatePosition = nma.updatePosition;
			bool updateRotation = nma.updateRotation;
			nma.updatePosition = false;
			nma.updateRotation = true;
			nma.SetDestination (position);
			nma.updatePosition = updatePosition;
			nma.updateRotation = updateRotation;*/
	}
}