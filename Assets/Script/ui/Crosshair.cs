using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : HUDElement {

	private Weapon weapon;

	override protected void Start() {
		base.Start();
		weapon = target.GetComponent<Weapon>();
	}

	// Update is called once per frame
	private void OnGUI() {
		transform.position = Input.mousePosition;
	}
}
