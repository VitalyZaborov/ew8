using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHuman : MonoBehaviour {

	private Plane hPlane;

	// Use this for initialization
	void Start () {
		hPlane = new Plane(Vector3.up, Vector3.zero);
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance = 0;
		if (hPlane.Raycast (ray, out distance)) {
			Vector3 worldPos = ray.GetPoint (distance);
			transform.LookAt(worldPos);
		}
	}
}
