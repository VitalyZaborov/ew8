using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour{

	private const float ANGLE_SCALING = 0.333f;
	// Update is called once per frame
	void LateUpdate () {
	//	transform.rotation.Set(0,-transform.parent.transform.rotation.y, 0, 0);
	//	transform.Rotate(0, 1, 0);
		float rotation = Mathf.DeltaAngle(Camera.main.transform.rotation.eulerAngles.y, transform.parent.transform.rotation.eulerAngles.y);
		rotation = rotation * ANGLE_SCALING  - (rotation >= 0 ? 90 * ANGLE_SCALING : 180 - 90 *ANGLE_SCALING);
		
		transform.rotation = Quaternion.Euler(0, rotation, 0);
	}
}
